using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Extensions
{
    public static class JWTSettings
    {
        public const string Key = "C1CF4B4DC1C4177B7618DE4F55CA2";
        public const string Issuer = "Project1640";
        public const string Audience = "Project1640.User";
        public const int DurationInMinutes = 120;
    }
}
