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

        // Setup-mutators (voor SetupWizard)
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Naam mag niet leeg zijn");
            Name = name.Trim();
        }

        public void SetProfilePicture(byte[] blob)
        {
            if (blob == null || blob.Length == 0)
                throw new ArgumentException("Profielfoto mag niet leeg zijn");
            ProfilePicture = blob;
        }
    }
}
