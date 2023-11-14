using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}