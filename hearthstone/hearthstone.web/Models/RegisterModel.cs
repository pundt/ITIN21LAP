using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.Models
{
    public class RegisterModel
    {
        public byte[] Avatar { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Mail { get; internal set; }
        public string Password { get; internal set; }
        public string Username { get; internal set; }
    }
}