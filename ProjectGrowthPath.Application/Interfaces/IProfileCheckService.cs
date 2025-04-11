using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface IProfileCheckService
    {
        Task CheckAndRedirectIfNeeded();
    }
}
