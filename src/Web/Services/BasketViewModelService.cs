﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IBasketService _basketService;

        public BasketViewModelService(IHttpContextAccessor httpContextAccessor, IAsyncRepository<Basket> basketRepository,IBasketService basketService)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketRepository = basketRepository;
            _basketService = basketService;
        }


        public async Task<int> GetOrCreateBasketIdAsync()
        {

            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Is there logged in user ? 
            if (!string.IsNullOrEmpty(userId))
            {
                //Does user have a basket ?

                var spec = new BasketSpecification(userId);
                Basket basket = await _basketRepository.FirstOrDefaultAsync(spec);
                if (basket != null)
                {
                    return basket.Id;
                }
                //If not, Create a new basket with the logged in user id and return its id.
                return (await CreateBasketAsync(userId)).Id;



            }
            //Is there a basket cookie
            var anonymousUserId = _httpContextAccessor.HttpContext.Request.Cookies[Constants.BASKET_COOKIENAME];
            if (!string.IsNullOrEmpty(anonymousUserId)) 
            {
                //Find the basket and return its id. 
                var spec = new BasketSpecification(anonymousUserId);
                Basket basket = await _basketRepository.FirstOrDefaultAsync(spec);
                return basket.Id;
            }

            //If not, Create a new basket with the anonymous User id and return its id.
            anonymousUserId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousUserId, new CookieOptions() { Expires = DateTime.Now.AddMonths(1), IsEssential = true });

            //return Id
            return (await CreateBasketAsync(anonymousUserId)).Id;
        }

        private async Task<Basket> CreateBasketAsync(string buyerId)
        {
            Basket basket = new Basket()
            {
                BuyerId = buyerId
            };

            return await _basketRepository.AddAsync(basket);
        }


        public async Task<BasketItemAddedViewModel> AddItemToBasketAsync(int productId, int quantity)
        {
            //Get or Create Basket id
            var basketId = await GetOrCreateBasketIdAsync();

            //Add İtem to the basket
            await _basketService.AddItemToBasketAsync(basketId, productId, quantity);

            //return items count in the basket
            return new BasketItemAddedViewModel()
            {
                ItemsCount = await _basketService.BasketItemsCountAsync(basketId)
            };
        }

        public async Task<NavbarBasketViewModel> GetNavbarBasketViewModelAsync()
        {

            return  new NavbarBasketViewModel()
            {
                ItemsCount = await BasketItemsCountAsync()
            };

        }
        //This method doesn't create a basket if non exist.
        private async Task<int> BasketItemsCountAsync()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var anonymousUserId = _httpContextAccessor.HttpContext.Request.Cookies[Constants.BASKET_COOKIENAME];
            //userId yi buyerId ye ata userId null sa anonymousId yi ata;
            var buyerId = userId ?? anonymousUserId;

            if (buyerId != null)
            {
                var spec = new BasketSpecification(buyerId);
                var basket = await _basketRepository.FirstOrDefaultAsync(spec);

                if (basket != null)
                    return await _basketService.BasketItemsCountAsync(basket.Id);
            }

            return 0;
        }
    }
}
