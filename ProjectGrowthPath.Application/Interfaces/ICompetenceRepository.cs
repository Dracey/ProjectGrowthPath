using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;
public interface ICompetenceRepository
{
    Task<Competence?> Get(int id);
    Task<List<Competence>> GetList();
    Task<Competence> Add(CompetenceCreateDto competence);
    Task Delete(int id);
    Task Update(int id, CompetenceDto competence);
}

