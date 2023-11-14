using System;
using System.Collections.Generic;

namespace SharedModels.Models
{
    public partial class WishList
    {
        public int WishListId { get; set; }

        public int? UserId { get; set; }

    }
}