using System;
using System.Collections.Generic;
using System.Text;

namespace BL.User
{
    public class BLUser
    {
        public DTO.User Fetch(string _email)
        {
            DAL.UserDB dalProvider = new DAL.UserDB();
            var user = dalProvider.GetUser(_email);
            var userDTO = new DTO.User(user);
            return userDTO;
        }

        public void Save(DTO.User user)
        {

        }
    }
}
