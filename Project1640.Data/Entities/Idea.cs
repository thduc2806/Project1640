using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Entities
{
    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubmissionId { get; set; }

        public Submission Submission { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public List<File> Files { get; set; }
        public List<Cmt> Cmts { get; set; }
        public List<View> Views { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}
