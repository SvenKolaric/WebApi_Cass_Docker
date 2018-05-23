using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Token
    {
        #region Constructors
        public Token()
        {

        }

        public Token(Entities.Token token)
        {
            this.Email = token.Email;
            this.TokenData = token.TokenData;
            this.ExpirationDate = token.ExpirationDate;
            this.CreationDate = token.CreationDate;
            this.DeviceName = token.DeviceName;
            this.Blacklisted = token.Blacklisted;
        }

        #endregion

        #region Properties

        public string Email { get; set; }
        public string TokenData { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string DeviceName { get; set; }
        public bool Blacklisted { get; set; }

        #endregion

        public Entities.Token ToEntity()
        {
            Entities.Token entity = new Entities.Token
            {
                Email = Email,
                TokenData = TokenData,
                ExpirationDate = ExpirationDate,
                CreationDate = CreationDate,
                DeviceName = DeviceName,
                Blacklisted = Blacklisted
            };
            return entity;
        }
    }
}
