using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Submissions
{
    public class SubmissionDto
    {
        public int SubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime FinalClosureDate { get; set; }
    }
}
