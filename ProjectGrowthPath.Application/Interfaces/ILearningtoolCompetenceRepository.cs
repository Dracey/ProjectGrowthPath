using ProjectGrowthPath.Application.DTOs.LearningToolCompetence;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;

public interface ILearningtoolCompetenceRepository
{
    Task<LearningToolCompetence?> Get(int id);
    Task<List<LearningToolCompetence>> GetList();
    Task<LearningToolCompetence> Add(LearningToolCompetenceCreateDto competence);
    Task Delete(int id);
    Task Update(int id, LearningToolCompetenceDto competence);
}
