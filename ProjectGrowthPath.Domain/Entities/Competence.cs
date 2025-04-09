using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Domain.Entities
{
    // Einiteit voor de competenties die worden toegevoegd aan het systeem.
    public class Competence
    {
        public Guid CompetenceID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
