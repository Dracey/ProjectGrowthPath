using ProjectGrowthPath.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces.IRepository;

public interface IUserCompetenceRepository
{
    Task AddUserCompetenceAsync(Guid userId, int competenceId, CompetenceType type);
    Task<IEnumerable<UserCompetence>> GetUserCompetencesAsync(Guid userId);
    Task RemoveUserCompetenceAsync(Guid userId, int competenceId);
}
