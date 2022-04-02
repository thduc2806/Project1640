using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime FinalClosureDate { get; set; }

        public List<Idea> Ideas { get; set; }
    }
}
