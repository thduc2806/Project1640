using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Ideas
{
	public class UpdateIdeaRequest
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public DateTime LastModifiedDate { get; set; }
	}
}
