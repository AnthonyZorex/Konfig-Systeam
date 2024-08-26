using System;
using System.IO;
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
