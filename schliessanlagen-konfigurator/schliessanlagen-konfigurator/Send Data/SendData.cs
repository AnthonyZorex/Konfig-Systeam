using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
namespace schliessanlagen_konfigurator.Send_Data
{
    public class SendData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new schliessanlagen_konfiguratorContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<schliessanlagen_konfiguratorContext>>()))
            {

            }
        }
    }
}
