﻿using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Macaw.Umbraco.Foundation.Mvc;
using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using DevTrends.MvcDonutCaching;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Controllers
{
	/// <summary>
	/// Default base controller for Umbraco projects.
	/// More about Route Hijacking: http://our.umbraco.org/documentation/Reference/Mvc/custom-controllers
	/// </summary>
	public class DynamicBaseController : Controller, IRenderMvcController // more info: http://our.umbraco.org/documentation/Reference/Mvc/custom-controllers
	{
		protected ISiteRepository Repository;

		public DynamicBaseController(ISiteRepository rep)
			: base()
        {
            Repository = rep;
        }

		[DonutOutputCache(Duration = 86400, VaryByCustom="url", Options = OutputCacheOptions.NoCacheLookupForPosts)]
		public virtual ActionResult Index(RenderModel model) //Template name, default is Index
		{
            return View(model.Content.As<DynamicModel>());
		}

        //http://our.umbraco.org/documentation/Reference/Mvc/custom-controllers
        //https://github.com/Shandem/Umbraco4Docs/blob/4.8.0/Documentation/Reference/Mvc/surface-controllers.md
        //http://our.umbraco.org/forum/developers/api-questions/38048-Umbraco-411-MVC-Custom-Routing-Content-is-null-How-can-I-load-content?p=1


		//TODO: add rss, using a virtual RSS template with no view.
		//public virtual ActionResult Rss(RenderModel model)
		//{
		//	return View(ControllerContext.RouteData.Values["action"].ToString(),
		//		new DynamicModel(model.Content));
		//}

		public virtual ActionResult Sitemap()
		{
			return new SitemapActionResult(Repository);
		}
	}
}