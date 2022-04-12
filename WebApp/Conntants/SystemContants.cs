using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Conntants
{
    public class SystemContants
    {
        public const string MainConnectionString = "DbConnection";
        public class AppSettings
        {
            public const string Jwt = "Jwt";
            public const string BaseAddress = "BaseAddress";
        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 4;
        }
    }
}
