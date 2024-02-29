using AutoMapper;
using LessonTime.Services.Catalog.Dtos;
using LessonTime.Services.Catalog.Models;
using LessonTime.Services.Catalog.Settings;
using LessonTime.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace LessonTime.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection; // Kurs koleksiyonunu temsil eden IMongoCollection<Course> alanı.
        private readonly IMongoCollection<Category> _categoryCollection; // Kategori koleksiyonunu temsil eden IMongoCollection<Category> alanı.
        private readonly IMapper _mapper; // AutoMapper nesnesini temsil eden IMapper alanı.
        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);// MongoDB istemcisini oluştur.

            
            var database = client.GetDatabase(databaseSettings.DatabaseName);// MongoDB veritabanını al.           
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);// Kurs koleksiyonunu al.       
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);// Kategori koleksiyonunu al.
            _mapper = mapper;// AutoMapper ile eşleme yapılabilmesi için IMapper'ı enjekte et.
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();// MongoDB'den tüm kursları al.


            // Eğer kurslar varsa, her bir kursun kategorisini belirlemek için ilgili kategori bilgisini de al.
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>(); // Eğer kurs yoksa, boş bir liste oluştur.

            }
            // Kursları CourseDto türüne dönüştürerek ve başarılı bir yanıtla döndür.
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();// Belirli bir kursu ID'ye göre MongoDB'den al.
            // Eğer kurs bulunamazsa, hata yanıtı döndür.
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            // Kursun kategorisini belirlemek için ilgili kategori bilgisini al.
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            // Kursu CourseDto türüne dönüştürerek ve başarılı bir yanıtla döndür.
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            // Belirli bir kullanıcıya ait tüm kursları MongoDB'den al.
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();
            // Eğer kurslar varsa, her bir kursun kategorisini belirlemek için ilgili kategori bilgisini de al.
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                // Eğer kurs yoksa, boş bir liste oluştur.
                courses = new List<Course>();
            }

            // Kursları CourseDto türüne dönüştürerek ve başarılı bir yanıtla döndür.
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        // Yeni bir kurs oluşturmak için metot:
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            // Yeni bir kurs oluştur ve MongoDB'ye ekle.
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreateTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            // Oluşturulan kursu CourseDto türüne dönüştürerek ve başarılı bir yanıtla döndür.
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            // Belirli bir kursu güncelle ve MongoDB'de değiştir.
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            // Eğer kurs bulunamazsa, hata yanıtı döndür.
            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
            // Başarılı bir yanıtla dön.
            return Response<NoContent>.Success(204);
        }

        // Belirli bir kursu silmek için metot:
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            // Belirli bir kursu sil ve MongoDB'den kaldır.
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            // Eğer kurs silinirse, başarılı bir yanıtla dön.
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                // Eğer kurs bulunamazsa, hata yanıtı döndür.
                return Response<NoContent>.Fail("Course not found", 404);
            }
        }
    }

}

