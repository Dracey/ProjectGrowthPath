using ProjectGrowthPath.Application.DTOs;

namespace ProjectGrowthPath.Application.Service.Enums
{
    /// <summary>
    /// Een basisservice voor het ophalen van enums.
    /// </summary>
    public class BaseEnumService<T> where T : Enum
    {
        public static List<EnumDto> GetEnumList()
        {
            return Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumDto
                {
                    Id = Convert.ToInt32(e),
                    Name = e.ToString()
                })
                .ToList();
        }
    }
}
