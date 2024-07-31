using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Org.BouncyCastle.Cms;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class Rehnungs
    {
        public int Id { get; set; }
        public string RehnungsId { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public List<IFormFile> File { get; set; }
        public int UserOrdersShopId { get; set; }
        public UserOrdersShop UserOrdersShop { get; set; }
    }
}
