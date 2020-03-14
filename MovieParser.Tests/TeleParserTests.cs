using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
namespace MovieParser.Tests
{
    [TestClass]
    public class TeleParserTests
    {
        private TeleParser sut = new TeleParser();
        [TestMethod]
        public void ShouldParseDirector()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);

            Assert.AreEqual("Timo Vuorensola", $"{newMovie.Director.FirstName} {newMovie.Director.LastName}");
        }

        [TestMethod]
        public void ShouldParseActors()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);
            Assert.AreEqual(3, newMovie.Actors.Count);
            Assert.AreEqual("Lara Rossi", $"{newMovie.Actors[0].FirstName} {newMovie.Actors[0].LastName}");
        }

        [TestMethod]
        public void ShouldParseMovieList()
        {
            var channels = new[] {"CanalPlus.pl", "HBO.pl" }.Select(c=>new Entities.Channel() { Name = c });
        
            var content = GetMovieListContent();
            var result = sut.ParseTvSchedule(channels, content);
            Assert.AreEqual(3, result.Count());
            //Assert.AreEqual("Lara Rossi", $"{newMovie.Actors[0].FirstName} {newMovie.Actors[0].LastName}");
        }

        private string GetMovieContent()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(Path.Combine(dir, "movieContent.txt"));
        }

        private string GetMovieListContent()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(Path.Combine(dir, "moviesList.txt"));
        }
    }
}
