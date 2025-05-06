using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.DTOs.LearningToolCompetence;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services;

/// <summary>
/// Repository class voor learningtool-competence.
/// </summary>
public class LearningToolCompetenceRepository : ILearningtoolCompetenceRepository
{
    private readonly AppDbContext _dbContext;

    public LearningToolCompetenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LearningToolCompetence?> Get(int id)
    {
        return await _dbContext.ToolCompetences.FindAsync(id);
    }
    public async Task<List<LearningToolCompetence>> GetList()
    {
        return await _dbContext.ToolCompetences.ToListAsync();
    }

    public async Task<LearningToolCompetence> Add(LearningToolCompetenceCreateDto input)
    {
        var newLearningToolCompetence = new LearningToolCompetence
        {
            CompetenceID = input.CompetenceID,
            LearningToolID = input.LearningToolID
        };

        var entityEntry = await _dbContext.ToolCompetences.AddAsync(newLearningToolCompetence);
        await _dbContext.SaveChangesAsync(); // Ensure the changes are saved to the database
        return entityEntry.Entity; // Return the LearningTool entity
    }

    public async Task Delete(int id)
    {
        await _dbContext.ToolCompetences
            .Where(x => x.LearningToolCompID == id)
            .ExecuteDeleteAsync();
    }

    public async Task Update(int id, LearningToolCompetenceDto competence)
    {
        throw new NotImplementedException();
    }
}
