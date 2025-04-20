using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.DTOs.LearningTools
{
    public class LearningToolCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DifficultyTool Difficulty { get; set; }
        public CategoryTool Category { get; set; }
        public int Duration { get; set; }
        public string Provider { get; set; } = string.Empty;
    }
}
