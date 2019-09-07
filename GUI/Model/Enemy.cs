using GUI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.Model
{
    ///<summary>
    ///Describes logic and move Enemies.
    ///</summary>
    public class Enemy
    {
        IGame Game { get; set; }
        ///<summary>
        ///Current coordinates Enemy.
        ///</summary>
        public int[] CurrentCoords { get; set; }
        ///<summary>
        ///Сoordinates on which the cell on which the enemy stands. It is necessary to memorize the cell on which the enemy stands.
        ///</summary>
        int[] CoordsNearCell { get; set; }
        ///<summary>
        ///Type of neighboring remembered cell.
        ///</summary>
        Cell NearCell { get; set; }

        public Enemy(IGame game, int[] Coords)
        {
            Game = game;
            CurrentCoords = Coords;
            CoordsNearCell = new int[] { Coords[0], Coords[1] };
            NearCell = Cell.Empty;
        }

        ///<summary>
        ///Needed to return a remembered cell to its place.
        ///</summary>
        private void ReturnNearCell(Cell[][] Maze)
        {
            if ((CoordsNearCell[0] != -1 && CoordsNearCell[1] != -1) && (CoordsNearCell[0] != 0 && CoordsNearCell[1] != 0))
                Maze[CoordsNearCell[0]][CoordsNearCell[1]] = NearCell;
            CoordsNearCell[0] = -1;
            CoordsNearCell[1] = -1;
        }

        public void Game_Right(Cell[][] Maze)
        {
            ReturnNearCell(Maze);
            if (Maze[CurrentCoords[0]][CurrentCoords[1] + 1] == Cell.Player)
                Game.Died();
            if (Maze[CurrentCoords[0]][CurrentCoords[1] + 1] == Cell.Enemy)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] + 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] + 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] + 1] == Cell.Coin)
            {
                NearCell = Cell.Coin;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] + 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] + 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] + 1] == Cell.Exit)
            {
                NearCell = Cell.Exit;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] + 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] + 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] + 1] == Cell.Empty)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] + 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] + 1] = Cell.Enemy;
            }
            CurrentCoords[1]++;
            Thread.Sleep(300);
        }

        public void Game_Left(Cell[][] Maze)
        {
            ReturnNearCell(Maze);
            if (Maze[CurrentCoords[0]][CurrentCoords[1] - 1] == Cell.Player)
                Game.Died();
            if (Maze[CurrentCoords[0]][CurrentCoords[1] - 1] == Cell.Enemy)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] - 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] - 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] - 1] == Cell.Coin)
            {
                NearCell = Cell.Coin;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] - 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] - 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] - 1] == Cell.Exit)
            {
                NearCell = Cell.Exit;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] - 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] - 1] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0]][CurrentCoords[1] - 1] == Cell.Empty)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0];
                CoordsNearCell[1] = CurrentCoords[1] - 1;
                Maze[CurrentCoords[0]][CurrentCoords[1] - 1] = Cell.Enemy;
            }
            CurrentCoords[1]--;
            Thread.Sleep(300);
        }

        public void Game_Down(Cell[][] Maze)
        {
            ReturnNearCell(Maze);
            if (Maze[CurrentCoords[0] + 1][CurrentCoords[1]] == Cell.Player)
                Game.Died();
            if (Maze[CurrentCoords[0] + 1][CurrentCoords[1]] == Cell.Enemy)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0] + 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] + 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] + 1][CurrentCoords[1]] == Cell.Coin)
            {
                NearCell = Cell.Coin;
                CoordsNearCell[0] = CurrentCoords[0] + 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] + 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] + 1][CurrentCoords[1]] == Cell.Exit)
            {
                NearCell = Cell.Exit;
                CoordsNearCell[0] = CurrentCoords[0] + 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] + 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] + 1][CurrentCoords[1]] == Cell.Empty)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0] + 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] + 1][CurrentCoords[1]] = Cell.Enemy;
            }
            CurrentCoords[0]++;
            Thread.Sleep(300);

        }

        public void Game_Up(Cell[][] Maze)
        {
            ReturnNearCell(Maze);
            if (Maze[CurrentCoords[0] - 1][CurrentCoords[1]] == Cell.Player)
                Game.Died();
            if (Maze[CurrentCoords[0] - 1][CurrentCoords[1]] == Cell.Enemy)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0] - 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] - 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] - 1][CurrentCoords[1]] == Cell.Coin)
            {
                NearCell = Cell.Coin;
                CoordsNearCell[0] = CurrentCoords[0] - 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] - 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] - 1][CurrentCoords[1]] == Cell.Exit)
            {
                NearCell = Cell.Exit;
                CoordsNearCell[0] = CurrentCoords[0] - 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] - 1][CurrentCoords[1]] = Cell.Enemy;
            }
            if (Maze[CurrentCoords[0] - 1][CurrentCoords[1]] == Cell.Empty)
            {
                NearCell = Cell.Empty;
                CoordsNearCell[0] = CurrentCoords[0] - 1;
                CoordsNearCell[1] = CurrentCoords[1];
                Maze[CurrentCoords[0] - 1][CurrentCoords[1]] = Cell.Enemy;
            }
            CurrentCoords[0]--;
            Thread.Sleep(300);
        }
    }
}
