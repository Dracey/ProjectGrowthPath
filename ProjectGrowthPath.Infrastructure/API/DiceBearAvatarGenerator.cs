using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Application.Interfaces;

namespace ProjectGrowthPath.Infrastructure.API
{
    public class DiceBearAvatarGenerator : IAvatarGenerator
    {
        private readonly HttpClient _httpClient;
        public DiceBearAvatarGenerator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<(string Seed, string Url)>> GenerateMultipleAvatarUrlsAsync(string style, int count)
        {
            var avatars = new List<(string Seed, string Url)>();
            for (int i = 0; i < count; i++)
            {
                var seed = Guid.NewGuid().ToString("N").Substring(0, 8);
                var url = $"https://api.dicebear.com/8.x/{style}/svg?seed={seed}";
                avatars.Add((seed, url));
            }
            return avatars;
        }

        public async Task<byte[]> GenerateAvatarAsync(string style, string seed)
        {
            var url = $"https://api.dicebear.com/8.x/{style}/svg?seed={seed}";
            return await _httpClient.GetByteArrayAsync(url);
        }
    }
}
