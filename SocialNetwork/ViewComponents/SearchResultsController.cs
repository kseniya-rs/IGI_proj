using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.ViewComponents
{
    public class SearchResultsViewComponent : ViewComponent
    {
        public IViewComponentResult Index()
        {
            return View();
        }
    }
}