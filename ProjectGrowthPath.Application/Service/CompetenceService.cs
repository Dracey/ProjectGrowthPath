using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

public class CompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;

    public CompetenceService(ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task<List<CompetenceDto>> GetAllCompetencesAsync()
    {
        var list = await _competenceRepository.GetAllAsync();
        return list.Select(c => new CompetenceDto
        {
            CompetenceID = c.CompetenceID,
            Name = c.Name,
            Description = c.Description,
            Category = c.Category
        }).ToList();
    }

    public async Task AddCompetenceAsync(CompetenceDto competenceDto)
    {
        var competence = new Competence
        {
            CompetenceID = competenceDto.CompetenceID,
            Name = competenceDto.Name,
            Description = competenceDto.Description,
            Category = competenceDto.Category
        };
        await _competenceRepository.AddAsync(competence);
    }
}

