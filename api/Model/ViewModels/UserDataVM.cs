using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class UserDataVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public bool IsAdmin { get; set; }
    }
}
