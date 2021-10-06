﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {

        Task AddItemToBasketAsync(int basketId, int productId, int quantity);

        Task<int> BasketItemsCountAsync(int basketid);

        Task SetQantitiesAsync(int BasketId, Dictionary<int,int> quantities );

        Task RemoveBasketItemAsync(int basketId, int basketItemId);

        Task DeleteBasketAsync(int basketId);

        Task TransferBasketAsync(string fromBuyerId, string toBuyerId );

    }
}
