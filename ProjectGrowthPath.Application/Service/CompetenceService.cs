using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

/// <summary>
/// Service voor alle leercompetenties.
/// </summary>
public class CompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;

    public CompetenceService(ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task<CompetenceDto> Get(int id)
    {
        var competence = await _competenceRepository.Get(id);

        return new CompetenceDto
        {
            Id = competence.CompetenceID,
            Name = competence.Name,
            Description = competence.Description,
            Category = competence.Category
        };
    }

    public async Task<List<CompetenceDto>> GetList()
    {
        var list = await _competenceRepository.GetList();
        return list.Select(c => new CompetenceDto
        {
            Id = c.CompetenceID,
            Name = c.Name,
            Description = c.Description,
            Category = c.Category
        }).ToList();
    }

    public async Task<Competence> Add(CompetenceCreateDto competenceDto)
    {
        return await _competenceRepository.Add(competenceDto);
    }

    public async Task Delete(int id)
    {
        await _competenceRepository.Delete(id);
    }
}

