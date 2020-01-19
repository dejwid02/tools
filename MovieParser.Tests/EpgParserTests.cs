using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieParser.Entities;

namespace MovieParser.Tests
{
    [TestClass]
    public class EpgParserTests
    {
        private EpgParser sut = new EpgParser();
        [TestMethod]
        public void ParseSingleMovieShouldReturnOneItem()
        {
            var channel = new Channel() { Name = "CanalPlusDiscovery.pl" };
            var movies = sut.ParseTvSchedule(new[] { channel }, epgMovie);
            Assert.AreEqual(1, movies.Count);
        }

        private string epgMovie = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<tv generator-info-name=""EPG Polska"" generator-info-url=""http://www.epg-polska.net"">
  <channel id=""CanalPlusDiscovery.pl"">
    <display-name lang=""pl"">Canal+ Dokument</display-name>
    <icon src=""http://www.epg-polska.net/logoTV/4_fun_dance.png"" />
    <url>http://www.epg-polska.net</url>
  </channel>
  <programme start=""20200118050000 +0100"" stop=""20200118070000 +0100"" channel=""CanalPlusDiscovery.pl"">
    <title lang=""pl"">Ksi�ga wielkich �ycze�</title>
    <desc lang=""pl"">Poruszaj�cy film ""Ksi�ga wielkich �ycze�"" jest dzie�em S�awomira Kry�skiego, autora m.in. znanych rodzimych komedii - ""Dziecko szcz�cia"" z 1991 roku, a z nowszych - ""To nie tak jak my�lisz, kotku"". Film ma bardzo dobr� obsad� i ciekawy scenariusz, kt�ry ukazuje zderzenie ludzi nale��cych do dw�ch �wiat�w - dzieci i starc�w u kresu swoich dni.
Akcja dramatu ""Ksi�ga wielkich �ycze�"" zawi�zuje si�, gdy w domu dziecka wybucha po�ar. Straty s� bardzo du�e. Sytuacja ta zmusza kierownictwo plac�wki do podj�cia niestandardowych decyzji. Kilka dziewczynek tymczasowo zostaje umieszczonych w miejscowym zak�adzie dla ludzi starych i przewlekle chorych. Wprowadza to niema�e zamieszanie w spokojne na og� �ycie pensjonariuszy o�rodka. 
Siedmioletnia otwarta i pogodna Marysia (Martyna Michalska) ca�� sw� mi�o�� przelewa na nie�mia�ego i zamkni�tego w sobie Adama (Gustaw Holoubek). Jednak jej natr�ctwo tylko irytuje zgorzknia�ego m�czyzn�, zak��caj�c mu samotno�� i spok�j. Nie przeszkadza za to sympatycznemu Henrykowi (Henryk Machalica), kt�ry lubi sp�dza� czas z dziewczynk� i zaprzyja�nia si� z ni�. Pewnego dnia jednak Henryk umiera. Marysia jest przera�ona i zaczyna ba� si�, �e to samo spotka Adama, z kt�rym nadal uporczywie pr�buje si� zaprzyja�ni�. Stopniowo przywi�zanie dziecka budzi w schorowanym m�czy�nie dawno zapomniane uczucia.(n)</desc>
    <credits>
      <director>S�awomir Kry�ski</director>
      <actor>Gustaw Holoubek</actor>
      <actor>Henryk Machalica</actor>
      <actor>Martyna Michalska</actor>
      <actor>Leon Niemczyk</actor>
      <actor>Danuta Szaflarska</actor>
      <actor>Katarzyna Skrzynecka</actor>
      <actor>Ewa Gawryluk</actor>
      <actor>Hanna Stank�wna</actor>
      <actor>Ignacy Machowski</actor>
      <actor>Micha� Pawlicki</actor>
    </credits>
    <date>1997</date>
    <category lang=""pl"">Dramat</category>
    <category lang=""pl"">Obyczajowy</category>
    <country lang=""pl"">Polska</country>
    <rating>
      <value>12</value>
    </rating>
    <star-rating>
      <value>3</value>
    </star-rating>
  </programme>
  </tv>";
    }
}