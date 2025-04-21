using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

/// <summary>
/// Service voor alle leermiddelen handelingen.
/// </summary>
public class LearningToolsService
{
    private ILearningToolsRepository _learningToolRepository;

    public LearningToolsService(ILearningToolsRepository learningToolRepository)
    {
        _learningToolRepository = learningToolRepository;
    }

    public async Task<LearningToolDto> Get(int id)
    {
        var learningTool = await _learningToolRepository.Get(id);

        return new LearningToolDto
        {
            Id = learningTool.LearningToolID,
            Name = learningTool.Name,
            Description = learningTool.Description,
            Link = learningTool.Link,
            Difficulty = learningTool.Difficulty,
            Category = learningTool.Category,
            Duration = learningTool.Duration,
            Provider = learningTool.Provider
        };
    }

    public async Task<List<LearningToolDto>> GetList()
    {
        var learningTools = await _learningToolRepository.GetList();

        return learningTools.Select(x => new LearningToolDto
        {
            Id = x.LearningToolID,
            Name = x.Name,
            Description = x.Description,
            Link = x.Link,
            Difficulty = x.Difficulty,
            Category = x.Category,
            Duration = x.Duration,
            Provider = x.Provider
        }).ToList();
    }
    public async Task<LearningTool> Add(LearningToolCreateDto dto)
    {
        return await _learningToolRepository.Add(dto);
    }

    public async Task Delete(int id)
    {
        await _learningToolRepository.Delete(id);
    }

    public Task Update(int id, LearningToolDto dto)
    {
        throw new NotImplementedException();
    }
}
