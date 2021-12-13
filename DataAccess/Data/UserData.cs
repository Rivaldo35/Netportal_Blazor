using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers()
        {
            var output = _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { }, "np");

            return output;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var output = await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id }, "np");

            return output.FirstOrDefault();
        }
        public Task InsertUser(UserModel user) => 
            _db.SaveData("dbo.spUser_Insert", new { user.instelling_id, user.voornaam, user.achternaam, user.email }, "np");

        public Task UpdateUser(UserModel user) =>
          _db.SaveData("dbo.spUser_Update", user , "np");

    }
}
