[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VoiceWall.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VoiceWall.Web.App_Start.NinjectWebCommon), "Stop")]

namespace VoiceWall.Web.App_Start
{
    using System;
    using System.Web;
    using System.Data.Entity;
    using System.Configuration;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using VoiceWall.Data;
    using VoiceWall.Data.Common.Repositories;

    using VoiceWall.CloudStorage.Common;
    using VoiceWall.CloudStorage.Dropbox;
    using VoiceWall.CloudStorage.TelerikBackend;

    using VoiceWall.Services.Common.Fetchers;
    using VoiceWall.Services.Common.Generators;
    using VoiceWall.Services.Common.Logic.Reactions;
    using VoiceWall.Services.Fetcher;
    using VoiceWall.Services.Generator;
    using VoiceWall.Services.Logic.Reactions;
    using VoiceWall.Services.Common.Users;
    using VoiceWall.Services.Users;
    using VoiceWall.Services.Common;

    using VoiceWall.Web.Infrastructure.Caching;
    using VoiceWall.Services.Common.Administration;
    using VoiceWall.Data.Models;
    using VoiceWall.Services.Administration;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            RegisterDatabaseServices(kernel);
            RegisterCloudStorageServices(kernel);
            RegisterServicesLayerServices(kernel);
        }

        private static void RegisterDatabaseServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<VoiceWallDbContext>();
            kernel.Bind<IVoiceWallDbContext>().To<VoiceWallDbContext>();
            kernel.Bind(typeof(IDeletableEntityRepository<>)).To(typeof(DeletableEntityRepository<>));
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IVoiceWallData>().To<VoiceWallData>();
        }

        private static void RegisterCloudStorageServices(IKernel kernel)
        {
            kernel.Bind<IPicturesCloudStorage>().To(ConfigurationManager.AppSettings["PicturesCloudStorage"] == null ?
                typeof(DropboxCloudStorage) : Type.GetType(ConfigurationManager.AppSettings["PicturesCloudStorage"]));

            kernel.Bind<ISoundsCloudStorage>().To(ConfigurationManager.AppSettings["SoundsCloudStorage"] == null ?
                typeof(DropboxCloudStorage) : Type.GetType(ConfigurationManager.AppSettings["SoundsCloudStorage"]));

            kernel.Bind<IVideosCloudStorage>().To(ConfigurationManager.AppSettings["VideosCloudStorage"] == null ?
                typeof(DropboxCloudStorage) : Type.GetType(ConfigurationManager.AppSettings["VideosCloudStorage"]));

            kernel.Bind<IUserProfilePicturesCloudStorage>().To(ConfigurationManager.AppSettings["UserProfilePicturesCloudStorage"] == null ?
                typeof(DropboxCloudStorage) : Type.GetType(ConfigurationManager.AppSettings["UserProfilePicturesCloudStorage"]));
        }

        private static void RegisterServicesLayerServices(IKernel kernel)
        {
            kernel.Bind<ICacheService>().To<InMemoryCache>();

            kernel.Bind<IContentFetcherService>().To<ContentFetcherService>();
            kernel.Bind<ICommentFetcherService>().To<CommentFetcherService>();

            kernel.Bind<ISoundUploadingGeneratorService>().To<SoundUploadingGeneratorService>();
            kernel.Bind<IVideoUploadingGeneratorService>().To<VideoUploadingGeneratorService>();
            kernel.Bind<IPictureUploadingGeneratorService>().To<PictureUploadingGeneratorService>();

            kernel.Bind<IContentReactionsService>().To<ContentReactionsService>();
            kernel.Bind<ICommentReactionsService>().To<CommentReactionsService>();

            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<ISearchResultsFetcherService>().To<SearchResultsFetcherService>();

            // admin
            kernel.Bind<IAdministrationService<Content>>().To<ContentAdministrationService>();
            kernel.Bind<IAdministrationService<Comment>>().To<CommentAdministrationService>();
            kernel.Bind<IAdministrationService<ContentView>>().To<ContentViewsAdministrationService>();
            kernel.Bind<IAdministrationService<CommentView>>().To<CommentViewsAdministrationService>();
        }
    }
}
