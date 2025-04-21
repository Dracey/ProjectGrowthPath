using ProjectGrowthPath.Domain.Enums.LearningTools;

namespace ProjectGrowthPath.Domain.Entities
{
    // Eniteit Class voor de leermiddelen in het systeem. 
    public class LearningTool
    {
        public int LearningToolID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DifficultyTool Difficulty { get; set; } = DifficultyTool.Easy;
        public CategoryTool Category { get; set; } = CategoryTool.Other;
        public int Duration { get; set; } // in minuten
        public string Provider { get; set; } = string.Empty;

        public ICollection<GoalLearningTool> GoalLearningTools { get; set; } = new List<GoalLearningTool>();
        public ICollection<LearningToolCompetence> ToolCompetences { get; set; } = new List<LearningToolCompetence>();
    }

}
