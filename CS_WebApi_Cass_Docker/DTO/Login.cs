using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Login
    {
        #region Constructors
        public Login()
        {

        }

        public Login(Entities.Login login)
        {
            this.Email = login.Email;
            this.Password = login.Password;
        }

        #endregion

        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        public Entities.Login ToEntity()
        {
            Entities.Login entity = new Entities.Login
            {
                Email = Email,
                Password = Password
            };
            return entity;
        }
    }
}
