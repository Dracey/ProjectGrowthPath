using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class ToolCompetence
    {
        public Guid ToolCompID { get; set; }
        public Guid LearningToolID { get; set; }
        public Guid CompetenceID { get; set; }


        // Navigatie eigenschap 
        public Competence Competence { get; set; }
        public LearningTool LearningTool { get; set; }

    }
}
