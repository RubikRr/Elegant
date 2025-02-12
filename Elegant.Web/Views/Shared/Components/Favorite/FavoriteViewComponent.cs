using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Views.Shared.Components.Favorite
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly IFavoritesStorage _favoritesStorage;

        public FavoriteViewComponent(IFavoritesStorage favoritesStorage)
        {
            _favoritesStorage = favoritesStorage;
        }

        public IViewComponentResult Invoke()
        {
            var userFavoriteProductsCount = _favoritesStorage.GetAllProducts(DbConstants.UserId).Count();
            if (userFavoriteProductsCount != 0)
            {
                return View("Favorite", userFavoriteProductsCount.ToString());
            }
            return View("Favorite", "");
        }
    }
}
