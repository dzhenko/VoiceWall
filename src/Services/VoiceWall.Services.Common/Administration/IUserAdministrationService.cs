namespace VoiceWall.Services.Common.Administration
{
    using System;
    using System.Collections.Generic;

    using VoiceWall.Data.Models;

    public interface IUserAdministrationService
    {
        IEnumerable<User> Read();

        IEnumerable<Guid> UpdateUser(string userId, bool IsAdmin, bool IsModerator, bool IsHidden);

        IEnumerable<Guid> DeleteUser(string userId);
    }
}
