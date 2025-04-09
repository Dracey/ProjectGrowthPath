using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class GoalLearningTool
    {
        public Guid GoalToolID { get; set; }
        public Guid GoalID { get; set; }
        public Guid LearningToolID { get; set; }
        public bool IsCompleted { get; set; }


        // Navigation eigenschappen
        public Goal Goal { get; set; }
        public LearningTool LearningTool { get; set; }
    }
}
