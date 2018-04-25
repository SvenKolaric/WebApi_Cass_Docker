using System;
using System.Collections.Generic;
using System.Text;
using Entities;


namespace DTO
{
    public class User
    {
        #region Constructors
        public User()
        {

        }

        public User(Entities.User user)
        {
            this.ID = user.ID;
            this.Email = user.Email;
            this.FullName = user.FullName;
            this.Password = user.Password;
            this.Salt = user.Salt;
            this.Role = user.Role;
        }

        #endregion

        #region Properties

        public Guid ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }

        #endregion

        public Entities.User ToEntity()
        {
            Entities.User entity = new Entities.User
            {
                ID = ID,
                Email = Email,
                FullName = FullName,
                Password = Password,
                Salt = Salt,
                Role = Role
            };
            return entity;
        }
    }
}
