using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface ILearningToolsService
    {
        Task<LearningToolDto> Get(int id);
        Task<List<LearningToolDto>> GetList();
        Task<LearningTool> Add(LearningToolCreateDto dto);
        Task Delete(int id);
        Task Update(int id, LearningToolDto dto);
    }
}
