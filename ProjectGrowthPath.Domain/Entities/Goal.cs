using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    // Entiteit class voor de goals die een gebruiker kan aanmaken. 
    public class Goal
    {
        public Guid GoalID { get; set; }
        public Guid UserID { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigatie eigenschap 
        public ICollection<GoalLearningTool> GoalLearningTools { get; set; } = new List<GoalLearningTool>();
        public UserProfile UserProfile { get; set; }

    }
}
