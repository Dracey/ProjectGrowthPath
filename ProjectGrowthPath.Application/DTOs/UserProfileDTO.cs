using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.DTOs;

public class UserProfileDto
{
    public Guid UserID { get; set; }
    public string ApplicationUserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Points { get; set; }
    public byte[]? ProfilePicture { get; set; }
}
