using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces.IRepository;

public interface IGoalRepository
{
    Task<Goal> Add(Goal newGoal);
    Task<Goal> GetById(int id);
    Task<IEnumerable<Goal>> GetAll();
    Task Update(Goal goal);
    Task Delete(int id);
}
