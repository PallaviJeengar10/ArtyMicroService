using SharedModels.Models;

namespace Arty.Controllers
{
    public class SubCategoryDetails
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
