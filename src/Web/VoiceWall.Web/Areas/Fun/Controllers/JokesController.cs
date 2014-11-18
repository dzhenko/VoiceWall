namespace VoiceWall.Web.Areas.Fun.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using VoiceWall.Services.Common.Fun;
    using VoiceWall.Web.Infrastructure.Caching;

    public class JokesController : Controller
    {
        private readonly IJokesFetherService jokeFetcherService;
        private readonly ICacheService cacheService;

        private readonly Random random;

        public JokesController(IJokesFetherService jokeFetcherService, ICacheService cacheService)
        {
            this.jokeFetcherService = jokeFetcherService;
            this.cacheService = cacheService;

            this.random = new Random();
        }

        [Authorize]
        public ActionResult GetRandomJoke()
        {
            var allJokes = this.cacheService.Get<IList<string>>("all-jokes-texts", 
                () => this.jokeFetcherService.GetJokesAsText().ToList());

            return this.Json(allJokes[random.Next(0, allJokes.Count)], JsonRequestBehavior.AllowGet);
        }
    }
}