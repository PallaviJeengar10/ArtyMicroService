using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Cart? Cart { get; set; }

    }
}