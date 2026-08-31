using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SolarSystemSolutionAlternative
{
    public class SolarItem
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        
        [JsonPropertyName("Type")]
        public SolarItemType Type { get; set; }


        
        public SolarItem(string description, SolarItemType solarItemType)
        {
            Description = description;
            Type = solarItemType;
        }

        [JsonConstructor]
        public SolarItem()
        {

        }
    }

    public enum SolarItemType { Star, Planet, Trabant }
}
