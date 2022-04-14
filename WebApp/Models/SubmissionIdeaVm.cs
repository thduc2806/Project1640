using Project1640.Dto.Ideas;
using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
	public class SubmissionIdeaVm
	{
		public SubmissionDto Submission { get; set; }
		public List<IdeaDto> Idea { get; set; }
	}
}
