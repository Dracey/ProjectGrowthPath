
using ProjectGrowthPath.Application.Interfaces.IRepository;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services;

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _dbContext;
    public GoalRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Goal> Add(Goal newGoal)
    {
        var entityEntry = await _dbContext.Goals.AddAsync(newGoal);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<Goal>> GetAll()
    {
        throw new NotImplementedException();
    }
    public Task<Goal> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public Task Update(Goal goal)
    {
        throw new NotImplementedException();
    }
}
