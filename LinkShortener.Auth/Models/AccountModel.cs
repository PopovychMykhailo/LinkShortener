using System;
using LinkShortener.Auth.Entities;

namespace LinkShortener.Auth.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }



        public static implicit operator AccountModel(AccountEntity entity)
        {
            return new AccountModel { Id = entity.Id, Email = entity.Email, Password = entity.Password, Role = entity.Role };
        }

        public static explicit operator AccountEntity(AccountModel model)
        {
            return new AccountEntity { Id = model.Id, Email = model.Email, Password = model.Password, Role = model.Role };
        }
    }
    
}
