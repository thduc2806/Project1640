using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
	public interface IIdeaApi
	{
		Task<List<IdeaDto>> GetAllIdeaBySubId(int subId);
		Task<IdeaDto> GetIdeaById(int ideaId);
		Task<bool> CreateIdea(CreateIdeaRequest request, int subId);
		Task<List<IdeaDto>> GetAllIdea();
	}
}
