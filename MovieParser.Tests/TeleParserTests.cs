using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

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

        private string GetMovieContent()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(Path.Combine(dir, "movieContent.txt"));
        }
    }
}
