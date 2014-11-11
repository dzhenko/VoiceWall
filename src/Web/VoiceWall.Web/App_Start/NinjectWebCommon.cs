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
    using VoiceWall.Data.Repositories;

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
            // Db
            kernel.Bind<DbContext>().To<VoiceWallDbContext>();
            kernel.Bind<IVoiceWallDbContext>().To<VoiceWallDbContext>();
            kernel.Bind(typeof(IDeletableEntityRepository<>)).To(typeof(DeletableEntityRepository<>));
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IVoiceWallData>().To<VoiceWallData>();

            // Cloud Storage
            kernel.Bind<IPicturesCloudStorage>().To(Type.GetType(ConfigurationManager.AppSettings["PicturesCloudStorage"]));
            kernel.Bind<ISoundsCloudStorage>().To(Type.GetType(ConfigurationManager.AppSettings["SoundsCloudStorage"]));
            kernel.Bind<IVideosCloudStorage>().To(Type.GetType(ConfigurationManager.AppSettings["VideosCloudStorage"]));
        }        
    }
}
