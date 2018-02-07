using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityAssistMVC.Models
{
    public class LoginClass
    {
        public LoginClass() { }
        public LoginClass(string user, string password)
        {
            UserName = user;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}