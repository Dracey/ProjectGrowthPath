namespace ProjectGrowthPath.Application.DTOs.LearningTools
{
    public class LearningToolCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public int Category { get; set; }
        public int Duration { get; set; }
        public string Provider { get; set; } = string.Empty;
        public List<int> Competences { get; set; } = new List<int>();
    }
}
