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
        public void ShouldParseRank()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);

            Assert.AreEqual(5.1, newMovie.Rating);
        }

        [TestMethod]
        public void ShouldParseUrl()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);

            Assert.AreEqual("http://www.filmweb.pl/film/Iron+Sky.+Inwazja-2019-727178", newMovie.Url);
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
        public void ShouldParseDescription()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);
            Assert.AreEqual("Wskutek wojny atomowej sprzed dwudziestu lat Ziemia została skażona. Zaledwie dwa tysiące ocalałych osób musiało opuścić planetę. Przenieśli się na Księżyc, gdzie zamieszkali w dawnej bazie nazistów, mającej kształt swastyki. Obi (Lara Rossi), córka Renaty Richter (Julia Dietze) i zmarłego Jamesa Washingtona, razem z kilkoma innymi śmiałkami, Sashą (Vladimir Burlakov), Donaldem (Tom Green) i Malcolmem (Kit Dale), rusza w podróż do wnętrza Ziemi, gdzie podobno znajduje się ratunek dla ludzkości. Okazuje się, że najbardziej zasłużone osoby w historii rasy ludzkiej to Vrilowie, czyli Reptilianie, jaszczuropodobne istoty. Dowodzi nimi sam Adolf Hitler (Udo Kier) dosiadający tyranozaura. Druga część parodystycznej serii filmów science fiction. Za reżyserię ponownie odpowiada Timo Vuorensola. Fundusze na realizację komedii zostały wyłożone przez fanów przez platformę Indiegogo.", newMovie.Description);
        }

        [TestMethod]
        public void ShouldParseImageUrl()
        {
            var newMovie = new Entities.Movie();
            var content = GetMovieContent();
            sut.FillMovieDetails(newMovie, content);
            Assert.AreEqual(@"//media.movieprovider.pl/photos/470x265/Iron-Sky-Inwazja2019.jpeg", newMovie.ImageUrl);
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
