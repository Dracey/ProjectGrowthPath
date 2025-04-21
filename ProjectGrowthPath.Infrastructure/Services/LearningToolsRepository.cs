using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.DTOs.LearningTools;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.Enums.LearningTools;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Application.Service
{
    public class LearningToolsRepository: ILearningToolsRepository
    {
        private readonly AppDbContext _dbContext;

        public LearningToolsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LearningTool> Get(int id)
        {
            return await _dbContext.LearningTools.FirstOrDefaultAsync(x => x.LearningToolID == id);
        }

        public async Task<List<LearningTool>> GetList()
        {
            return await _dbContext.LearningTools.ToListAsync();

        }
        public async Task<LearningTool> Add(LearningToolCreateDto dto)
        {
            var newLearningTool = new LearningTool
            {
                Name = dto.Name,
                Description = dto.Description,
                Link = dto.Link,
                Difficulty = (DifficultyTool)dto.Difficulty,
                Category = (CategoryTool)dto.Category,
                Duration = dto.Duration,
                Provider = dto.Provider
            };

            var entityEntry = await _dbContext.LearningTools.AddAsync(newLearningTool);
            await _dbContext.SaveChangesAsync(); // Ensure the changes are saved to the database
            return entityEntry.Entity; // Return the LearningTool entity
        }

        public async Task Delete(int id)
        {
            await _dbContext.LearningTools
                .Where(x => x.LearningToolID == id)
                .ExecuteDeleteAsync();
        }

        public Task Update(int id, LearningToolDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
