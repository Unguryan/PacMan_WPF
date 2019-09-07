using GUI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class Player : IPlayer
    {
        public IGame Game { get; set; }
        public int Score { get; set; }
        public event GamePlay Died;

        public Player(IGame game)
        {
            Score = 0;
            Game = game;
            Game.UpPlayer += Game_Up;
            Game.DownPlayer += Game_Down;
            Game.LeftPlayer += Game_Left;
            Game.RightPlayer += Game_Right;
        }

        private void RestartGame()
        {
            Score = 0;
            Died();
        }

        private void Game_Right(int[] coords, Cell[][] Maze)
        {
            if (coords[0] == -1 && coords[1] == -1) RestartGame();
            if (Maze[coords[0]][coords[1] + 1] != Cell.Wall)
            {
                if (Maze[coords[0]][coords[1] + 1] == Cell.Enemy)
                    RestartGame();
                if (Maze[coords[0]][coords[1] + 1] == Cell.Coin)
                    Score += 10;
                Maze[coords[0]][coords[1]] = Cell.Empty;
                Maze[coords[0]][coords[1] + 1] = Cell.Player;
            }
        }

        private void Game_Left(int[] coords, Cell[][] Maze)
        {
            if (coords[0] == -1 && coords[1] == -1) RestartGame();
            if (Maze[coords[0]][coords[1] - 1] != Cell.Wall)
            {
                if (Maze[coords[0]][coords[1] - 1] == Cell.Enemy)
                    RestartGame();
                if (Maze[coords[0]][coords[1] - 1] == Cell.Coin)
                    Score += 10;
                Maze[coords[0]][coords[1]] = Cell.Empty;
                Maze[coords[0]][coords[1] - 1] = Cell.Player;
            }
        }

        private void Game_Down(int[] coords, Cell[][] Maze)
        {
            if (coords[0] == -1 && coords[1] == -1) RestartGame();
            if (Maze[coords[0] + 1][coords[1]] != Cell.Wall)
            {
                if (Maze[coords[0] + 1][coords[1]] == Cell.Enemy)
                    RestartGame();
                if (Maze[coords[0] + 1][coords[1]] == Cell.Coin)
                    Score += 10;
                Maze[coords[0]][coords[1]] = Cell.Empty;
                Maze[coords[0] + 1][coords[1]] = Cell.Player;
            }
        }

        private void Game_Up(int[] coords, Cell[][] Maze)
        {
            if (coords[0] == -1 && coords[1] == -1) RestartGame();
            if (Maze[coords[0] - 1][coords[1]] != Cell.Wall)
            {
                if (Maze[coords[0] - 1][coords[1]] == Cell.Enemy)
                    RestartGame();
                if (Maze[coords[0] - 1][coords[1]] == Cell.Coin)
                    Score += 10;
                Maze[coords[0]][coords[1]] = Cell.Empty;
                Maze[coords[0] - 1][coords[1]] = Cell.Player;
            }
        }
    }
}
