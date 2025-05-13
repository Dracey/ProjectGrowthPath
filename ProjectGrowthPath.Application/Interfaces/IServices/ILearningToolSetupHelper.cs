using ProjectGrowthPath.Domain.Enums.LearningTools;

namespace ProjectGrowthPath.Application.Interfaces.IServices;
public interface ILearningToolSetupHelper
{
    public string GetDifficultyLabel(DifficultyTool difficulty);
    public string GetCategoryOfTool(CategoryTool category);
    public string GetImagePath(CategoryTool category);
}

