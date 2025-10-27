using LandingAppFolhetos.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace LandingAppFolhetos.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = "/")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Redirect("/login-failed");

            // Obtenha o email do utilizador Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            // Exemplo: só permite emails do domínio "minhaempresa.pt"
            if (string.IsNullOrEmpty(email) || !email.EndsWith("@minhaempresa.pt", StringComparison.OrdinalIgnoreCase))
            {
                // Opcional: pode mostrar uma página de acesso negado
                return Redirect("/acesso-negado");
            }

            // (Opcional) Lista de emails permitidos
            // var emailsPermitidos = new[] { "admin@minhaempresa.pt", "user@minhaempresa.pt" };
            // if (!emailsPermitidos.Contains(email, StringComparer.OrdinalIgnoreCase))
            //     return Redirect("/acesso-negado");

            // Fluxo normal do Identity
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
                return LocalRedirect(returnUrl ?? "/");

            // Se não existir conta, cria automaticamente
            var user = new ApplicationUser { UserName = email, Email = email };
            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }

            return Redirect("/login-failed");
        }
    }
}
