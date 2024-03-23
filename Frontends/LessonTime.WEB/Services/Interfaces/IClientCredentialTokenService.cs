using System.Threading.Tasks;
using System;

namespace LessonTime.WEB.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();

    }
}
