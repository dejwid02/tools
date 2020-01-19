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
    <title lang=""pl"">Ksiêga wielkich ¿yczeñ</title>
    <desc lang=""pl"">Poruszaj¹cy film ""Ksiêga wielkich ¿yczeñ"" jest dzie³em S³awomira Kryñskiego, autora m.in. znanych rodzimych komedii - ""Dziecko szczêœcia"" z 1991 roku, a z nowszych - ""To nie tak jak myœlisz, kotku"". Film ma bardzo dobr¹ obsadê i ciekawy scenariusz, który ukazuje zderzenie ludzi nale¿¹cych do dwóch œwiatów - dzieci i starców u kresu swoich dni.
Akcja dramatu ""Ksiêga wielkich ¿yczeñ"" zawi¹zuje siê, gdy w domu dziecka wybucha po¿ar. Straty s¹ bardzo du¿e. Sytuacja ta zmusza kierownictwo placówki do podjêcia niestandardowych decyzji. Kilka dziewczynek tymczasowo zostaje umieszczonych w miejscowym zak³adzie dla ludzi starych i przewlekle chorych. Wprowadza to niema³e zamieszanie w spokojne na ogó³ ¿ycie pensjonariuszy oœrodka. 
Siedmioletnia otwarta i pogodna Marysia (Martyna Michalska) ca³¹ sw¹ mi³oœæ przelewa na nieœmia³ego i zamkniêtego w sobie Adama (Gustaw Holoubek). Jednak jej natrêctwo tylko irytuje zgorzknia³ego mê¿czyznê, zak³ócaj¹c mu samotnoœæ i spokój. Nie przeszkadza za to sympatycznemu Henrykowi (Henryk Machalica), który lubi spêdzaæ czas z dziewczynk¹ i zaprzyjaŸnia siê z ni¹. Pewnego dnia jednak Henryk umiera. Marysia jest przera¿ona i zaczyna baæ siê, ¿e to samo spotka Adama, z którym nadal uporczywie próbuje siê zaprzyjaŸniæ. Stopniowo przywi¹zanie dziecka budzi w schorowanym mê¿czyŸnie dawno zapomniane uczucia.(n)</desc>
    <credits>
      <director>S³awomir Kryñski</director>
      <actor>Gustaw Holoubek</actor>
      <actor>Henryk Machalica</actor>
      <actor>Martyna Michalska</actor>
      <actor>Leon Niemczyk</actor>
      <actor>Danuta Szaflarska</actor>
      <actor>Katarzyna Skrzynecka</actor>
      <actor>Ewa Gawryluk</actor>
      <actor>Hanna Stankówna</actor>
      <actor>Ignacy Machowski</actor>
      <actor>Micha³ Pawlicki</actor>
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