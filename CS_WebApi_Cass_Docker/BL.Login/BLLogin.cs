using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Login
{
    public class BLLogin
    {
        public DTO.User CheckLogin(Entities.Login _login)
        {
            BL.User.BLUser blProvider = new BL.User.BLUser();
            var _user = blProvider.Fetch(_login.Email);

            if (_user != null)
            {
                //tu ceš morati desifrirati/sifrirati sifru?
                if ((_user.Password == _login.Password) && (_user.Email == _login.Email))
                {
                    return _user;
                }
            }

            return null;
         }
    }
}
