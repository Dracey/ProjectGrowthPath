using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    // Einiteit voor de competenties die worden toegevoegd aan het systeem.
    public class Competence
    {
        public int CompetenceID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CompetenceCategory Category { get; set; } = CompetenceCategory.SoftSkill;

        public enum CompetenceCategory
        {
            SoftSkill = 0,
            HardSkill = 1,
        }
    }
}
