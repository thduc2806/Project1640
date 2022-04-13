using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Files
{
	public class FilesAddRequest
	{
		//public DateTime CreatedDate { get; set; }
		public IFormFile Files { get; set; }
	}
}
