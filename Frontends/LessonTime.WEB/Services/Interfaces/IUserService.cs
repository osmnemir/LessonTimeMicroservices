using LessonTime.WEB.Models;
using System.Threading.Tasks;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();

    }
}
