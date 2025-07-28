using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Achive
{
    public class ImportUser
    {
        [Name("CPF")]
        public string? UserOfficialNumber { get; set; }

        [Name("NOME")]
        public string? UserName { get; set; }

        [Name("EMAIL")]
        public string? UserEmail { get; set; }

        [Name("SENHA")]
        public string? UserPassword { get; set; }

        [Name("SEXO")]
        public string? UserGender { get; set; }

        [Name("IDADE")]
        public int? UserAge { get; set; }

        [Name("STATUS")]
        public bool? Status { get; set; }

        [Name("RESULTADO")]
        public string? FinalResult { get; set; }
    }
}
