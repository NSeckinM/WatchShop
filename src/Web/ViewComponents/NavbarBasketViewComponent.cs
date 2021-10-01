using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;
//https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0#walkthrough-creating-asimple-view-component

namespace Web.ViewComponents
{
    public class NavbarBasketViewComponent : ViewComponent
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public NavbarBasketViewComponent(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return  View(await _basketViewModelService.GetNavbarBasketViewModelAsync());

        }

    }
}
