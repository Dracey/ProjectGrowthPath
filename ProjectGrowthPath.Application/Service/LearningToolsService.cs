using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service
{
    public class LearningToolsService : ILearningToolsService
    {
        public Task<LearningTool> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LearningTool>> GetList()
        {
            throw new NotImplementedException();
        }
        public Task<LearningTool> Add(LearningToolCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, LearningToolDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
