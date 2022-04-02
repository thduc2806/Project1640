using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class Cmt
    {
        public int CmtId { get; set; }
        public string content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }

        public User User { get; set; }
        public Idea Idea { get; set; }
    }
}
