using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class LearningToolCompetence
    {
<<<<<<< HEAD:ProjectGrowthPath.Domain/Entities/ToolCompetence.cs
        public int ToolCompID { get; set; }
=======
        public int LearningToolCompID { get; set; }
>>>>>>> e65e385defb07e54630a21a4f7603355db8b0046:ProjectGrowthPath.Domain/Entities/LearningToolCompetence.cs
        public int LearningToolID { get; set; }
        public int CompetenceID { get; set; }


        // Navigatie eigenschap 
        public Competence Competence { get; set; }
        public LearningTool LearningTool { get; set; }

    }
}
