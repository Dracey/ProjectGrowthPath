using ProjectGrowthPath.Application.Interfaces.IServices;
using ProjectGrowthPath.Domain.Enums.LearningTools;

namespace ProjectGrowthPath.Application.Service;
public class LearningToolSetupHelper : ILearningToolSetupHelper
{
    public string GetDifficultyLabel(DifficultyTool difficulty)
    {
        return difficulty switch
        {
            DifficultyTool.Easy => "Gemakkelijk",
            DifficultyTool.Medium => "Gemiddeld",
            DifficultyTool.Hard => "Moeilijk",
            _ => "Onbekend"
        };
    }

    public string GetCategoryOfTool(CategoryTool category)
    {
        return category switch
        {
            CategoryTool.Article => "Artikel",
            CategoryTool.Book => "Boek",
            CategoryTool.Course => "Cursus",
            CategoryTool.Game => "Spel",
            CategoryTool.Video => "Video",
            CategoryTool.Podcast => "Podcast",
            CategoryTool.Other => "Overig"
        };
    }

    public string GetImagePath(CategoryTool category)
    {
        var fileName = category.ToString().ToLower() + ".png"; // "article.png", etc.
        return $"tools-images/{fileName}";
    }
}
