namespace VoiceWall.Services.Common.Fun
{
    using System;
    using System.Collections.Generic;

    using VoiceWall.Data.Models;

    public interface IJokesFetherService
    {
        IEnumerable<string> GetJokesAsText();

        IEnumerable<Joke> GetJokes();

        void AddJoke(string text);

        void EditJoke(Guid id, string text);

        void DeleteJoke(Guid id);
    }
}
