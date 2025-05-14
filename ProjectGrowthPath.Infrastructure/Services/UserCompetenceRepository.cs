using ProjectGrowthPath.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services;

public class UserCompetenceRepository : IUserCompetenceRepository
{

    private readonly AppDbContext _dbContext;
    public UserCompetenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    // Voeg competenties toe aan de gebruiker
    public async Task AddUserCompetenceAsync(Dictionary<int, Competence> competences, Guid userid, int type)
    {
        foreach (var comptence in competences)
        {
            var userCompetence = new UserCompetence
            {
                CompetenceID = comptence.Key,
                UserID = userid,
                Type = type == 0 ? CompetenceType.Interest : CompetenceType.Skill
            };
            
            await _dbContext.UserCompetences.AddAsync(userCompetence);
        }
    }


    // Haal de competenties van de gebruiker op
    public Task<IEnumerable<UserCompetence>> GetUserCompetencesAsync(Guid userId)
    {
        // Implement the logic to get user competences
        throw new NotImplementedException();
    }

    // Verwijder de competentie van de gebruiker
    public Task RemoveUserCompetenceAsync(Guid userId, int competenceId)
    {
        // Implement the logic to remove a user competence
        throw new NotImplementedException();
    }
}
