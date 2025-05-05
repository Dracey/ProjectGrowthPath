using ProjectGrowthPath.Domain.Enums.Competences;
using System.IO;

namespace ProjectGrowthPath.Application.DTOs.Competences;

public class CompetenceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CompetenceCategory Category { get; set; } = CompetenceCategory.SoftSkill;

    //Required for MudBlazore multiselect
    public override bool Equals(object o)
    {
        var other = o as CompetenceDto;
        return other?.Id == Id;
    }
    public override int GetHashCode() => Id.GetHashCode();
}
