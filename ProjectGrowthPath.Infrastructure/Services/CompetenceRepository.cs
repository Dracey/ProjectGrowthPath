using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services;
public class CompetenceRepository : ICompetenceRepository
{

    private readonly AppDbContext _context;

    public CompetenceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Competence competence)
    {
        _context.Competences.Add(competence);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var competence = await _context.Competences.FindAsync(id);
        if (competence != null)
        {
            _context.Competences.Remove(competence);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Competence>> GetAllAsync()
    {
        return await _context.Competences.ToListAsync();
    }

    public async Task<Competence?> GetByIdAsync(int id)
    {
        return await _context.Competences.FindAsync(id);
    }

    public async Task UpdateAsync(Competence competence)
    {
        _context.Competences.Update(competence);
        await _context.SaveChangesAsync();
    }
}
