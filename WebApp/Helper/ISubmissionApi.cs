using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
	public interface ISubmissionApi
	{
		Task<List<SubmissionDto>> GetAllSub();
		Task<SubmissionDto> GetSubById(int subId);
		Task<bool> CreateSub(SubmissionAddRequest subRequest);
	}
}
