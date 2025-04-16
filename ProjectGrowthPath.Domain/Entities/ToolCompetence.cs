using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class ToolCompetence
    {
        public int ToolCompID { get; set; }
        public int LearningToolID { get; set; }
        public int CompetenceID { get; set; }


        // Navigatie eigenschap 
        public Competence Competence { get; set; }
        public LearningTool LearningTool { get; set; }

    }
}
