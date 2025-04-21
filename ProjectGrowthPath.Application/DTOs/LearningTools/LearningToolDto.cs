using ProjectGrowthPath.Domain.Enums.LearningTools;

namespace ProjectGrowthPath.Application.DTOs.LearningTools
{
    public class LearningToolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DifficultyTool Difficulty { get; set; }
        public CategoryTool Category { get; set; }
        public int Duration { get; set; }
        public string Provider { get; set; }
    }
}
