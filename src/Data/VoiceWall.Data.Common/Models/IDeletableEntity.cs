namespace VoiceWall.Data.Common.Models
{
    using System;

    public interface IDeletableEntity : IAuditInfo
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
