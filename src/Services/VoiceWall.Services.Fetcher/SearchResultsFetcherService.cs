namespace VoiceWall.Services.Fetcher
{
    using System.Linq;

    using VoiceWall.Data;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Common.Fetchers;

    public class SearchResultsFetcherService : ISearchResultsFetcherService
    {
        private IVoiceWallData data;

        public SearchResultsFetcherService(IVoiceWallData data)
        {
            this.data = data;
        }

        public IQueryable<User> SearchUsers(string search)
        {
            return this.data.Users.All()
                .Where(u => !u.IsHidden && (u.FirstName.ToLower().Contains(search.ToLower()) ||
                                            u.LastName.ToLower().Contains(search.ToLower())))
                .OrderBy(u => u.FirstName);
        }

        public IQueryable<Content> SearchAll(string search)
        {
            return this.data.Contents.All()
                .Where(c => !c.IsHidden && (c.User.FirstName.ToLower().Contains(search.ToLower()) ||
                                            c.User.LastName.ToLower().Contains(search.ToLower())))
                .OrderByDescending(c => c.CreatedOn);
        }

        public IQueryable<Content> SearchVideos(string search)
        {
            return this.data.Contents.All()
                .Where(c => !c.IsHidden && c.ContentType == ContentType.Video &&
                    (c.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.User.LastName.ToLower().Contains(search.ToLower())))
                .OrderByDescending(c => c.CreatedOn);
        }

        public IQueryable<Content> SearchVoices(string search)
        {
            return this.data.Contents.All()
                .Where(c => !c.IsHidden && c.ContentType == ContentType.Sound &&
                    (c.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.User.LastName.ToLower().Contains(search.ToLower())))
                .OrderByDescending(c => c.CreatedOn);
        }

        public IQueryable<Content> SearchPictures(string search)
        {
            return this.data.Contents.All()
                .Where(c => !c.IsHidden && c.ContentType == ContentType.Picture &&
                    (c.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.User.LastName.ToLower().Contains(search.ToLower())))
                .OrderByDescending(c => c.CreatedOn);
        }
    }
}
