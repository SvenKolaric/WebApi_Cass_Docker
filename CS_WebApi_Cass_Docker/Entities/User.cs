using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class User
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
