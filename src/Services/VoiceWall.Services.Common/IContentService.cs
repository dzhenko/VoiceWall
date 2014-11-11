namespace VoiceWall.Services.Common
{
    using System;
    using VoiceWall.Data.Models;

    public interface IContentService
    {
        bool Create(Content content);

        bool Delete(Guid id);

        bool Update(Content content);        
    }
}
