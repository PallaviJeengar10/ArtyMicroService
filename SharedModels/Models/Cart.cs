using System;
using System.Collections.Generic;

namespace SharedModels.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }

        public int? UserId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
    }
}