using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MovieParser
{
    public class TeleParser
    {
        public TeleParser()
        {

        }

        public IList<TvListingItem> ParseTvSchedule(IEnumerable<Channel> channels, string content)
        {
            var elemnt = XElement.Parse(content);

            return new List<TvListingItem>();
        }
    }
}
