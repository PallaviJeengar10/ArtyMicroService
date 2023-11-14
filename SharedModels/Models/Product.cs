using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int? SubCategoryId { get; set; }

    [JsonIgnore]
    public virtual SubCategory? SubCategory { get; set; }
}
}

