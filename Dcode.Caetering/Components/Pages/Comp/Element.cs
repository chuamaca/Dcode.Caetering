using System.Text.Json.Serialization;

namespace Dcode.Caetering.Components.Pages.Comp
{
    public class Element
    {
        public string Group { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        [JsonPropertyName("small")]
        public string Sign { get; set; }
        public double Molar { get; set; }
        public IList<int> Electrons { get; set; }

        public override string ToString()
        {
            return $"{Sign} - {Name}";
        }
    }
}
