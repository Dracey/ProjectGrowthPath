using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.DTOs
{
    public class CompetenceDto
    {
        public int CompetenceID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Competence.CompetenceCategory Category { get; set; }
    }
}
