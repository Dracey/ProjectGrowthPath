using ProjectGrowthPath.Domain.Enums.Competences;

namespace ProjectGrowthPath.Domain.Entities;

// Entiteit voor de competenties die worden toegevoegd aan het systeem.
public class Competence
{
    public int CompetenceID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CompetenceCategory Category { get; set; } = CompetenceCategory.SoftSkill;
}
