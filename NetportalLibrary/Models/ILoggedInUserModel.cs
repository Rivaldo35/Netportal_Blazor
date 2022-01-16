using System;

namespace Netportal.Library.Models
{
    public interface ILoggedInUserModel
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
        void ResetUserModel();
    }
}