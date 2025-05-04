using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;

public interface IFirstTimeSetupService
{
    Task UpdateNameAsync(string name);
    Task SetProfilePictureAsync(string style, string seed);
    Task UpdateCompetenceDictionary(int id, Competence? competence, string type);
    Task SetGoalCompetenceAsync(Competence competence);
    Task SetTargetDateAsync(DateTime date);
    Task FinalizeSetupAsync();
}

