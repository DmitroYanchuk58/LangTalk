﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.MVVM.Model
{
    public class RegistrationModel
    {
        public string Email { get; set; }

        public string Password { get; set; } 

        public string Nickname { get; set; }    

        public string ConfirmPassword { get; set; }
    }
}
