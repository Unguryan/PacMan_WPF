using GUI.Model;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace GUI.BLL.Interfaces
{
    public delegate void MovePlayer(int[] coords, Cell[][] Maze);
    public delegate void MoveEnemy(Cell[][] Maze);
    public delegate void GamePlay();
    public delegate void ShowMaze(Cell[][] Maze);

    ///<summary>
    ///An interface that describes the logic of the PacMan game.
    ///</summary>
    public interface IGame
    {
        ///<summary>
        ///The event is needed to turn character Up.
        ///</summary>
        event MovePlayer UpPlayer;
        ///<summary>
        ///The event is needed to turn character down.
        ///</summary>
        event MovePlayer DownPlayer;
        ///<summary>
        ///The event is needed to turn character left.
        ///</summary>
        event MovePlayer LeftPlayer;
        ///<summary>
        ///The event is needed to turn the character right.
        ///</summary>
        event MovePlayer RightPlayer;
        ///<summary>
        ///An event is needed to draw a level.
        ///</summary>
        event ShowMaze Show;
        ///<summary>
        ///The event will be triggered when level was completed.
        ///</summary>
        event GamePlay CompleteLevel;
        ///<summary>
        ///The event will be triggered when either the levels or the game is over.
        ///</summary>
        event GamePlay EndLevels;
        ///<summary>
        ///The event will be triggered to restart the level.
        ///</summary>
        event GamePlay RestartLevel;

        ///<summary>
        ///Current Player of game. 
        ///</summary>
        IPlayer Player { get; set; }
        ///<summary>
        ///List of enemies that are at this level.
        ///</summary>
        List<Enemy> Enemies { get; set; }
        ///<summary>
        ///Current level of game.
        ///</summary>
        int Level { get; set; }
        ///<summary>
        ///This Key is need to remember the last key pressed.
        ///</summary>
        Key KeyPressed { get; set; }
        ///<summary>
        ///This thread is necessary for the proper movement of enemies.
        ///</summary>
        Thread MoveEnemies { get; set; }
        ///<summary>
        ///This is necessary to check the running game. If True, then the game is in the process. Otherwise, no.
        ///</summary>
        bool InGame { get; set; }
        ///<summary>
        ///Map that the character goes through.
        ///</summary>
        Cell[][] Maze { get; set; }

        ///<summary>
        ///This method is designed to run the game.
        ///</summary>
        void Start();

        void StartAsync();

        ///<summary>
        ///The method is needed to restart the level.
        ///</summary>
        void Died();
    }
}