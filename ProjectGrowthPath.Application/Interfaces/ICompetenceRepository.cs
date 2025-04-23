using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;
public interface ICompetenceRepository
{
    Task<List<Competence>> GetAllAsync();
    Task<Competence?> GetByIdAsync(int id);
    Task AddAsync(Competence competence);
    Task UpdateAsync(Competence competence);
    Task DeleteAsync(int id);
}

