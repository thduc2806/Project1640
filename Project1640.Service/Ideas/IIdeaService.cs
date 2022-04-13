using Project1640.Dto.Files;
using Project1640.Dto.Ideas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Ideas
{
    public interface IIdeaService
    {
        Task<int> CreateIdea(CreateIdeaRequest request);
        Task<IdeaDto> GetIdeaById(int ideaId);
        Task<int> AddImage(int ideaId, FilesAddRequest request);
        Task<FilesDto> GetFileById(int fileId);
        Task<List<IdeaDto>> GetAllIdeaBySubId(int subId);
    }
}
