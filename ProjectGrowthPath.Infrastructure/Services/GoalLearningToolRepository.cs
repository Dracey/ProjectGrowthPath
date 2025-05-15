using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Application.Interfaces.IRepository;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services
{
    public class GoalLearningToolRepository : IGoalLearningToolRepository
    {
        private readonly AppDbContext _dbContext;
        public GoalLearningToolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GoalLearningTool> Add(List<int> goalLearningToolId, int goalid)
        {
            GoalLearningTool? lastAdded = null;

            foreach (var learningToolId in goalLearningToolId)
            {
                var goalLearning = new GoalLearningTool
                {
                    GoalID = goalid,
                    LearningToolID = learningToolId
                };

                var entityEntry = await _dbContext.GoalLearningTools.AddAsync(goalLearning);

                lastAdded = entityEntry.Entity;
            }

            await _dbContext.SaveChangesAsync();

            return lastAdded!;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GoalLearningTool>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GoalLearningTool> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(GoalLearningTool goalLearningTool)
        {
            throw new NotImplementedException();
        }
    }
}
