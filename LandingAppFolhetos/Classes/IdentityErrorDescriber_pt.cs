using Microsoft.AspNetCore.Identity;

namespace LandingAppFolhetos.Classes
{
    public class IdentityErrorDescriber_pt : IdentityErrorDescriber
    {
        // 1. Erro de Nome de Utilizador Duplicado
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"Erro: O nome de utilizador '{userName}' já está em uso."
                // Ou para o seu exemplo: Description = $"Erro: O Email '{userName}' já está registado."
            };
        }

        // 2. Erro de Email Duplicado
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"Erro: O endereço de email '{email}' já está em uso."
            };
        }

        // 3. Erro de Requisitos de Senha (Exemplo de uma regra)
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z')."
            };
        }

        // Adicione outros métodos conforme necessário (PasswordTooShort, InvalidToken, etc.)
    }
}