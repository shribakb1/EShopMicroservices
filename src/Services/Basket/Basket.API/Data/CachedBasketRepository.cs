﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
        : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            await repository.DeleteBasketAsync(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cacheBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
            }

            var basket = await repository.GetBasketAsync(userName, cancellationToken);
            
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);  

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
    }
}
