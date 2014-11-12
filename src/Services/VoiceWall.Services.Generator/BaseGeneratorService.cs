namespace VoiceWall.Services.Generator
{
    using VoiceWall.CloudStorage.Common;
    using VoiceWall.Data;

    public abstract class BaseGeneratorService
    {
        private readonly IVoiceWallData data;

        public BaseGeneratorService(IVoiceWallData data)
        {
            this.data = data;
        }

        protected IVoiceWallData Data
        {
            get { return this.data; }
        }
    }
}
