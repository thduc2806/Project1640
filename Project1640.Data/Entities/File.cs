using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class File
    {
        public int FileId { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdeaId { get; set; }

        public Idea Idea { get; set; }
    }
}
