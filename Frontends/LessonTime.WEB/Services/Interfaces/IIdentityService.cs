using IdentityModel.Client;
using LessonTime.Shared.Dtos;
using LessonTime.WEB.Models;
using System.Threading.Tasks;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();

    }
}
