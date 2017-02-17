namespace TakAI.Models
{
    public class CapStone : Stone
    {
        public CapStone(Color stoneColor) : base(stoneColor)
        {

        }
        // can move on top of standing stone
        // will flatten standing stone
        // counts as road
    }
}