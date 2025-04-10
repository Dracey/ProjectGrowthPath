using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    // Gebruikers en hun informatie die worden verwerkt in profielen. 
    public class UserProfile
    {
        public Guid UserID { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Points { get; set; }
        public byte[]? ProfilePicture { get; set; }


        // Navigatie eigenschap
        public ICollection<UserCompetence> Competences { get; set; } = new List<UserCompetence>();
        public ICollection<UserBadge> Badges { get; set; } = new List<UserBadge>();
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}
