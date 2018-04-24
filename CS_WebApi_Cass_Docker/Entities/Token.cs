using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Token
    {
        public string Email { get; set; }
        public string TokenData { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
