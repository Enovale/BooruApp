using System;
using System.IO;
using System.Reflection;

namespace BooruApp.Services
{
    public class CacheService : ICacheService
    {

        /*private async Task<Bitmap> GetImageAsync()
        {
            Directory.CreateDirectory(CacheDirectory);
            var split = PreviewUrl.Split('/').Last().Split('.');
            var filename = $"{split[^2]}.{split[^1]}";
            var cachePath = Path.Join(CacheDirectory, filename);
    
            if (File.Exists(cachePath))
            {
                return new Bitmap(cachePath);
            }
            
            using var client = new HttpClient();
            using var response = await client.GetAsync(PreviewUrl);
            await using var stream = await response.Content.ReadAsStreamAsync();
            var bitmap = new Bitmap(stream);
            bitmap.Save(cachePath);
            return bitmap;
        }*/
    }
}