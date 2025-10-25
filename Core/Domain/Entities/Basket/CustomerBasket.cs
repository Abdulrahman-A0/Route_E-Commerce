﻿namespace Domain.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public ICollection<BasketItem> BasketItems { get; set; } = [];
    }
}
