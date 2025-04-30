using ProjectGrowthPath.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface ICompetenceSelectionService
    {
        Task InitializeAsync();
        Competence? GetSelectedCompetence(int number, string mode);
        IEnumerable<Competence> GetAvailableCompetences(int currentNumber, string mode);
        Task SelectCompetenceAsync(int number, Competence? competence, string mode);
        bool CanProceed(string mode);
        Task<IEnumerable<Competence>> SearchCompetencesAsync(string value, string mode);
    }
}
