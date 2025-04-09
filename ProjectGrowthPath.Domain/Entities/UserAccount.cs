using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProjectGrowthPath.Domain.Entities
{
    public enum AuthProvider
    {
        Local,
        External
    }
    public class UserAccount
    {
        Guid ID { get; set; }
        public AuthProvider AuthProvider { get; set; } = AuthProvider.Local;
        public string? UserName { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
