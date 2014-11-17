namespace VoiceWall.Services.Moderation
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using VoiceWall.Data;
    using VoiceWall.Data.Common.Models;
    using VoiceWall.Services.Common.Administration;

    public abstract class BaseModerationService
    {
        private readonly IVoiceWallData data;

        public BaseModerationService(IVoiceWallData data)
        {
            this.data = data;
        }

        protected IVoiceWallData Data
        {
            get { return this.data; }
        }
    }
}
