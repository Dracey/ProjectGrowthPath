using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface IAvatarGenerator
    {
        Task<byte[]> GenerateAvatarAsync(string style, string seed);
        Task<List<(string Seed, string Url)>> GenerateMultipleAvatarUrlsAsync(string style, int count);
    }
}
