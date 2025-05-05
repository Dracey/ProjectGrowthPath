using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services;
public class CompetenceRepository : ICompetenceRepository
{

    private readonly AppDbContext _dbContext;

    public CompetenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Competence?> Get(int id)
    {
        return await _dbContext.Competences.FindAsync(id);
    }

    public async Task<List<Competence>> GetList()
    {
        return await _dbContext.Competences.ToListAsync();
    }

    public async Task<Competence> Add(CompetenceCreateDto competence)
    {
        var newCompetence = new Competence
        {
            Name = competence.Name,
            Description = competence.Description,
            Category = competence.Category
        };

        var entityEntry = await _dbContext.Competences.AddAsync(newCompetence);
        await _dbContext.SaveChangesAsync(); // Ensure the changes are saved to the database
        return entityEntry.Entity; // Return the LearningTool entity
    }

    public async Task Delete(int id)
    {
        await _dbContext.Competences
            .Where(x => x.CompetenceID == id)
            .ExecuteDeleteAsync();
    }

    public async Task Update(int Id, CompetenceDto competenceDto)
    {
        var competence = await _dbContext.Competences.FindAsync(Id);

        if (competence == null)
        {
            throw new Exception("Competence not found");
        }

        _dbContext.Competences.Update(competence);
        await _dbContext.SaveChangesAsync();
    }
}
