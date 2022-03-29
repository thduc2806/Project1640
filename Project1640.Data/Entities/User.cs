using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Gender { get; set; }

        public List<Idea> Ideas { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Cmt> Cmts { get; set; }
        public List<View> Views { get; set; }
    }
}
