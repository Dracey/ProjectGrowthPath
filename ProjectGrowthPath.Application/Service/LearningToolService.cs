using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Application.DTOs.LearningToolCompetence;
using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

/// <summary>
/// Service voor alle leermiddelen handelingen.
/// </summary>
public class LearningToolService
{
    private ILearningToolsRepository _learningToolRepository;
    private readonly LearningToolCompetenceService _learningToolCompetenceService;
    private readonly CompetenceService _competenceService;

    public LearningToolService(ILearningToolsRepository learningToolRepository, LearningToolCompetenceService learningToolCompetenceService, CompetenceService competenceService)
    {
        _learningToolRepository = learningToolRepository;
        _learningToolCompetenceService = learningToolCompetenceService;
        _competenceService = competenceService;
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
            Difficulty = (int)learningTool.Difficulty,
            Category = (int)learningTool.Category,
            Duration = learningTool.Duration,
            Provider = learningTool.Provider
        };
    }

    public async Task<List<LearningToolDto>> GetList()
    {
        var learningTools = await _learningToolRepository.GetList();
        var learningToolsDto = learningTools.Select(x => new LearningToolDto
        {
            Id = x.LearningToolID,
            Name = x.Name,
            Description = x.Description,
            Link = x.Link,
            Difficulty = (int)x.Difficulty,
            Category = (int)x.Category,
            Duration = x.Duration,
            Provider = x.Provider
        }).ToList();

        foreach (var learningTool in learningToolsDto)
        {
            var learningToolCompetences = await _learningToolCompetenceService.GetByLearningToolId(learningTool.Id);
            var competences = new List<CompetenceDto>();
            foreach (var learningToolCompetence in learningToolCompetences)
            {
                var competence = await _competenceService.Get(learningToolCompetence.CompetenceID);
                competences.Add(new CompetenceDto
                {
                    Id = competence.Id,
                    Name = competence.Name,
                    Description = competence.Description,
                    Category = competence.Category
                });
            }
            learningTool.Competences = competences;
        }

        return learningToolsDto;
    }

    public async Task<LearningTool> Add(LearningToolCreateDto dto)
    {
        var learningtool =  await _learningToolRepository.Add(dto);

        if(dto.Competences != null)
        {
            foreach (var competence in dto.Competences)
            {
                await _learningToolCompetenceService.Add(new LearningToolCompetenceCreateDto
                {
                    CompetenceID = competence,
                    LearningToolID = learningtool.LearningToolID
                });
            }
        }

        return learningtool;
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
