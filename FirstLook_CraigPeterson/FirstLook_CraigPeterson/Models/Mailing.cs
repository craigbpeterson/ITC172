using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstLook_CraigPeterson.Models
{
    public class Mailing
    {
        public string FirstName { get; set; } //get; and set; are c# shorthand
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}