using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces.IRepository;

public interface IGoalLearningToolRepository
{
    public Task<GoalLearningTool> Add(List<int> goalLearningTool, int goalid);
    public Task Delete(int id);
    public Task<IEnumerable<GoalLearningTool>> GetAll();
    public Task<GoalLearningTool> GetById(int id);
    public Task Update(GoalLearningTool goalLearningTool);


}
