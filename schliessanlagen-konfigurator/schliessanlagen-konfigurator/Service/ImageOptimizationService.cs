using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using TinifyAPI;
namespace schliessanlagen_konfigurator.Service
{
    public class ImageOptimizationService
    {
        public ImageOptimizationService()
        {
            Tinify.Key = "Adaej_Uv0p5lPaT3d9fmivS0BrRvOdm1"; 
        }
        public async Task CompressImageAsync(string inputPath, string outputPath)
        {
            try
            {
                var source = Tinify.FromFile(inputPath);
                await source.ToFile(outputPath);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка во время минификации изображения: " + ex.Message);
            }
        }
    }
}
