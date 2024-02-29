using LessonTime.Services.Catalog.Dtos;
using LessonTime.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LessonTime.Services.Catalog.Services
{
    internal interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        Task<Response<CategoryDto>> GetByIdAsync(string id);

    }
}
