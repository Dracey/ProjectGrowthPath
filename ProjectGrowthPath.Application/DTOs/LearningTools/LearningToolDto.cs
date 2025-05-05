using ProjectGrowthPath.Application.DTOs.Competences;
using ProjectGrowthPath.Domain.Enums.LearningTools;

namespace ProjectGrowthPath.Application.DTOs.LearningTools
{
    public class LearningToolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Difficulty { get; set; }
        public int Category { get; set; }
        public int Duration { get; set; }
        public string Provider { get; set; }
        public IEnumerable<CompetenceDto> Competences { get; set; }
    }
}
