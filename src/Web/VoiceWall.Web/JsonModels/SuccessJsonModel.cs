namespace VoiceWall.Web.JsonModels
{
    public class SuccessJsonModel
    {
        public static SuccessJsonModel Succeeded
        {
            get
            {
                return new SuccessJsonModel() { Success = true };
            }
        }

        public static SuccessJsonModel Failed
        {
            get
            {
                return new SuccessJsonModel() { Success = false };
            }
        }

        public bool Success { get; set; }
    }
}