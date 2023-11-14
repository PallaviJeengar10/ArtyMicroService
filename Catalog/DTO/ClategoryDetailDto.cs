using SharedModels.Models;

namespace Arty.Dtos
{
    public class ClategoryDetailDto
    {
        public int CategoryId {get; set;}
        public string CategoryName { get; set;}
        public List<SubCategory> SubCategory { get; set;}
    }
}
