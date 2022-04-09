using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Dto.Exceptions
{
	public class Project1640Exception : Exception
	{
		public Project1640Exception()
		{
		}
		public Project1640Exception(string message) : base(message)
		{
		}
		public Project1640Exception(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
