using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class View
    {
        public int ViewId { get; set; }
        public DateTime LastVisitedDate { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }

        public User User { get; set; }
        public Idea Idea { get; set; }
    }
}
