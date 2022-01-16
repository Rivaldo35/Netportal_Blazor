using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netportal.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string? Id { get; set; }
        public int instelling_id { get; set; }
        public string? voornaam { get; set; }
        public string? achternaam { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? status { get; set; }
        public string? internal_user { get; set; }
        public int failed_attempts { get; set; }
        public DateTime? pwd_exp_date { get; set; }
        public DateTime? pwd_changed_date { get; set; }

        public void ResetUserModel()
        {
            Token = "";
            Id = "";
            instelling_id = 0;
            voornaam = "";
            achternaam = "";
            username = "";
            email = "";
            status = "";
            internal_user = "";
            failed_attempts = 0;
            pwd_exp_date = DateTime.MinValue;
            pwd_changed_date = DateTime.MinValue;
        }
    }

}
