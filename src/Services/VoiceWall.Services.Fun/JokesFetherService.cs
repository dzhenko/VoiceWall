namespace VoiceWall.Services.Fun
{
    using System;
using System.Collections.Generic;
using System.Linq;
using VoiceWall.Data;
using VoiceWall.Data.Models;
using VoiceWall.Services.Common.Fun;

    public class JokesFetherService : IJokesFetherService
    {
        private IVoiceWallData data;

        public JokesFetherService(IVoiceWallData data)
        {
            this.data = data;
        }

        public IEnumerable<string> GetJokesAsText()
        {
            return this.data.Jokes.All().Select(j => j.Text);
        }

        public IEnumerable<Joke> GetJokes()
        {
            return this.data.Jokes.All();
        }

        public void AddJoke(string text)
        {
            this.data.Jokes.Add(new Joke() { Text = text });
            this.data.SaveChanges();
        }

        public void EditJoke(Guid id, string text)
        {
            var joke = this.data.Jokes.GetById(id);
            joke.Text = text;
            this.data.Jokes.Update(joke);
            this.data.SaveChanges();
        }

        public void DeleteJoke(Guid id)
        {
            this.data.Jokes.Delete(id);
            this.data.SaveChanges();
        }
    }
}
