using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{

    public partial class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
