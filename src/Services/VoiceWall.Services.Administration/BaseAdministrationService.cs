namespace VoiceWall.Services.Administration
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using VoiceWall.Data;
    using VoiceWall.Data.Common.Models;
    using VoiceWall.Services.Common.Administration;

    public abstract class BaseAdministrationService
    {
        private readonly IVoiceWallData data;

        public BaseAdministrationService(IVoiceWallData data)
        {
            this.data = data;
        }

        protected IVoiceWallData Data
        {
            get { return this.data; }
        }
    }
}
