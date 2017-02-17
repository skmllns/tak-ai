using System.Linq;

namespace TakAI.Models
{
    public enum Color { Black, White }
    public abstract class Stone
    {
        public Stone(Color stoneColor)
        {
            StoneColor = stoneColor;
        }
        public Color StoneColor { get; set; }
      
    }
}