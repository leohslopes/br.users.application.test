using br.users.application.test.domain.Entities.Achive;
using FluentValidation;
using System.Text.RegularExpressions;

namespace br.users.application.test.application.Validators
{
    public class UserModelValidator : AbstractValidator<ImportUser>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.UserOfficialNumber)
                .NotEmpty().WithMessage("O CPF é obrigatório.");
                //.Must(predicate: BeValidCpf).WithMessage("CPF inválido.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(80).WithMessage("O nome deve ter no máximo 80 caracteres.");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage("A senha é obrigatória")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.");
                //.Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\_\=\+\{\}\[\]\:\;\<\>\,\.\?\/\\]").WithMessage("A senha deve conter ao menos um caractere especial.");

            RuleFor(x => x.UserGender)
                .NotEmpty().WithMessage("O sexo é obrigatório.")
                .Must(g => g.ToUpper().Trim().Equals("M") || g.ToUpper().Trim().Equals("F")).WithMessage("Sexo deve ser \"M\"(Masculino) ou \"F\"(Feminino).");

            RuleFor(x => x.UserAge)
                .NotNull().WithMessage("A idade é obrigatória.")
                .InclusiveBetween(0, 120).WithMessage("A idade deve estar entre 0 e 120 anos.");
        }

        private bool BeValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            // Primeiro dígito
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += numbers[i] * (10 - i);

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;
            if (numbers[9] != digit1)
                return false;

            // Segundo dígito
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += numbers[i] * (11 - i);

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;
            return numbers[10] == digit2;
        }
    }
}
