using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Files
{
	public class FilesDto
	{
		public int FileId { get; set; }
		public int IdeaId { get; set; }
		public string FilePath { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
