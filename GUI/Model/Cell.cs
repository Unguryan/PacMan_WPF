using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    ///<summary>
    ///Describes all the possible cells that are needed to display and interact with the game.
    ///</summary>
    public enum Cell
    {
        Empty = 0,
        Wall = 1,
        Coin = 2,
        Player = 3,
        Entry = 4,
        Enemy = 5,
        Exit = 6
    }
}
