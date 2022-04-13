using Project1640.Dto.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Submissions
{
    public interface ISubmissionService
    {
        Task<List<SubmissionDto>> GetAllSub();
        Task<List<SubmissionDto>> GetSubById(int subId);
    }
}
