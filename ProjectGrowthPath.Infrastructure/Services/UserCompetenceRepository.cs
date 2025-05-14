using ProjectGrowthPath.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Infrastructure.Services;

public class UserCompetenceRepository : IUserCompetenceRepository
{
    public Task AddUserCompetenceAsync(Guid userId, int competenceId, CompetenceType type)
    {
        // Implement the logic to add a user competence
        throw new NotImplementedException();
    }
    public Task<IEnumerable<UserCompetence>> GetUserCompetencesAsync(Guid userId)
    {
        // Implement the logic to get user competences
        throw new NotImplementedException();
    }
    public Task RemoveUserCompetenceAsync(Guid userId, int competenceId)
    {
        // Implement the logic to remove a user competence
        throw new NotImplementedException();
    }
}
