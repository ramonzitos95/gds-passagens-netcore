using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.Models.Dto
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string DataNascimento { get; set; }
        public string Departamento { get; set; }
        public string ImagemUrlUser { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
    }
}