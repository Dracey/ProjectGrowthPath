using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    public class UserBadge
    {
        public Guid UserBadgeID { get; set; }
        public Guid UserID { get; set; }
        public string BadgeName { get; set; } = string.Empty;
        public DateTime Received { get; set; }

        // Navigatie eigenschap 
        public UserProfile User { get; set; }

    }
}