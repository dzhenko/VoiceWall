namespace VoiceWall.Data.Models
{
    using System;

    using VoiceWall.Data.Common.Models;

    public class Joke : DeletableEntity
    {
        public Joke()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
