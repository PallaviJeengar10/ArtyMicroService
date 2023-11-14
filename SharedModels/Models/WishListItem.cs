using System;
using System.Collections.Generic;

namespace SharedModels.Models
{
    public partial class WishListItem
    {
        public int WishListItemId { get; set; }

        public int? WishListId { get; set; }

        public int? ProductId { get; set; }

        public virtual Cart? WishList { get; set; }
    }
}