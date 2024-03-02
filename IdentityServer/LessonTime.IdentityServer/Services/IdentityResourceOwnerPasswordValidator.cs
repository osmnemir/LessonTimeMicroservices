using IdentityModel;
using IdentityServer4.Validation;
using LessonTime.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LessonTime.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Kullanıcının e-posta adresine göre kullanıcıyı bul
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            // Kullanıcı bulunamadıysa
            if (existUser == null)
            {
                // Hataları temsil eden bir sözlük oluştur
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                // Özel bir yanıt ayarla ve işlemi sonlandır
                context.Result.CustomResponse = errors;
                return;
            }

            // Kullanıcının şifresini kontrol et
            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);

            // Şifre yanlışsa
            if (passwordCheck == false)
            {
                // Hataları temsil eden bir sözlük oluştur
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                // Özel bir yanıt ayarla ve işlemi sonlandır
                context.Result.CustomResponse = errors;
                return;
            }

            // Kullanıcı doğrulandıysa, kimlik doğrulama sonucunu ve kullanıcı kimliğini döndür
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }

    }
}
