using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Application.DTOs.LearningToolCompetence;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

public class LearningToolCompetenceService
{
    private readonly ILearningtoolCompetenceRepository _learningToolCompetenceRepository;

    public LearningToolCompetenceService(ILearningtoolCompetenceRepository learningToolCompetenceRepository)
    {
        _learningToolCompetenceRepository = learningToolCompetenceRepository;
    }

    public async Task<LearningToolCompetenceDto> Get(int id)
    {
        var learningtoolCompetence = await _learningToolCompetenceRepository.Get(id);

        return new LearningToolCompetenceDto
        {
            LearningToolCompID = learningtoolCompetence.LearningToolCompID,
            CompetenceID = learningtoolCompetence.CompetenceID,
            LearningToolID = learningtoolCompetence.LearningToolID,
        };
    }

    public async Task<List<LearningToolCompetenceDto>> GetList()
    {
        var list = await _learningToolCompetenceRepository.GetList();
        return list.Select(e => new LearningToolCompetenceDto
        {
            LearningToolCompID = e.LearningToolCompID,
            CompetenceID = e.CompetenceID,
            LearningToolID = e.LearningToolID,
        }).ToList();
    }

    public async Task<List<LearningToolCompetenceDto>> GetByLearningToolId(int learningToolId)
    {
        var list = await _learningToolCompetenceRepository.GetList();
        return list.Where(e => e.LearningToolID == learningToolId).Select(e => new LearningToolCompetenceDto
        {
            LearningToolCompID = e.LearningToolCompID,
            CompetenceID = e.CompetenceID,
            LearningToolID = e.LearningToolID,
        }).ToList();
    }

    public async Task<LearningToolCompetence> Add(LearningToolCompetenceCreateDto learningToolCompetenceDto)
    {
        return await _learningToolCompetenceRepository.Add(learningToolCompetenceDto);
    }
}
