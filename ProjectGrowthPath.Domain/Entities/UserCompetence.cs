using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class UserCompetence
    {
        public int UserCompID { get; set; }
        public Guid UserID { get; set; }
        public int CompetenceID { get; set; }
        public CompetenceType Type { get; set; } = CompetenceType.Interest;

        public UserProfile User { get; set; }
        public Competence Competence { get; set; }
    }

    public enum CompetenceType
    {
        Interest = 0,
        Skill = 1
    }

}
