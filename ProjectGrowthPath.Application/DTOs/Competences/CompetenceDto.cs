using ProjectGrowthPath.Domain.Enums.Competences;

namespace ProjectGrowthPath.Application.DTOs.Competences;

public class CompetenceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CompetenceCategory Category { get; set; } = CompetenceCategory.SoftSkill;
}
