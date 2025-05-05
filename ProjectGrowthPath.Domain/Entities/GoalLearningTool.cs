using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class GoalLearningTool
    {
        public int GoalToolID { get; set; }
        public int GoalID { get; set; }
        public int LearningToolID { get; set; }
        public bool IsCompleted { get; set; }


        // Navigation eigenschappen
        public Goal Goal { get; set; }
        public LearningTool LearningTool { get; set; }
    }
}
