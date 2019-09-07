using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.BLL.Interfaces
{
    public interface IPlayer
    {
        IGame Game { get; set; }

        ///<summary>
        ///Сharacter score.
        ///</summary>
        int Score { get; set; }

        ///<summary>
        ///The event is triggered when the character dies.
        ///</summary>
        event GamePlay Died;
    }
}
