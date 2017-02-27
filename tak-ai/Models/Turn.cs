using System.Collections.Generic;
using System.Data.SqlTypes;
using TakAI.Models;

namespace TakAI.Models
{
    
    public abstract class Turn
    {
        public string TurnNotation;
        public string TargetLocation;
    }
}

//move + gamestate = new gamestate
