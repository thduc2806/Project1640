﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Common
{
	public interface IStorageService
	{
		string GetFileUrl(string fileName);

		Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

		Task DeleteFileAsync(string fileName);
	}
}
