using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class Reaction
    {
        public int ReactionId { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }

        public User User { get; set; }
        public Idea Idea { get; set; }
    }
}
