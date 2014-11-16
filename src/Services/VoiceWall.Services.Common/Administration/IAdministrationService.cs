namespace VoiceWall.Services.Common.Administration
{
    using System.Collections.Generic;

    using VoiceWall.Data.Common.Models;

    public interface IAdministrationService<T> where T : IDeletableEntity
    {
        void Create(T entity);

        void Delete(object id);

        void Update(T entity);

        IEnumerable<T> Read();

        T Get(object id);
    }
}
