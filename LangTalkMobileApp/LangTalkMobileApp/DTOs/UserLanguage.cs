using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.DTOs
{
    public class UserLanguage
    {
        public Guid IdUser { get; set; }

        public Guid IdLanguage { get; set; }

        public bool IsSpoken { get; set; }

        public string Level { get; set; } = null!;
    }
}
