using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface ILearningToolsRepository
    {
        Task<LearningTool> Get(int id);
        Task<List<LearningTool>> GetList();
        Task<LearningTool> Add(LearningToolCreateDto dto);
        Task Delete(int id);
        Task Update(int id, LearningToolDto dto);
    }
}
