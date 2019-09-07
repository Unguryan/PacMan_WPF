using GUI.BLL;
using GUI.BLL.Interfaces;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModel
{
    public class GameWindowViewModel : ViewModelBase, IGame
    {
        public GameWindowViewModel()
        {
            Player = new Player(this);
            Player.Died += Died;
            Level = 1;
        }

        private bool moveEnemy;

        public IPlayer Player { get; set; }
        public int Level { get; set; }
        public Thread MoveEnemies { get; set; }
        public bool InGame { get; set; }
        public Cell[][] Maze { get; set; }
        public List<Enemy> Enemies { get; set; }
        public object KeyPressed { get; set; }

        public event ShowMaze Show;
        public event GamePlay CompleteLevel;
        public event GamePlay EndLevels;
        public event GamePlay RestartLevel;
        public event MovePlayer UpPlayer;
        public event MovePlayer DownPlayer;
        public event MovePlayer LeftPlayer;
        public event MovePlayer RightPlayer;

        public void Died()
        {
            Maze = MazeRead.ReadLevel(Level);
            MoveEnemies.Abort();
            Level = 1;
            InGame = false;
            moveEnemy = false;
            KeyPressed = null;
            RestartLevel();
            Start();
        }

        private void MoveEnemy()
        {
            CreateEnemies();
            while (moveEnemy)
            {
                if (Enemies.Count != 0)
                {
                    foreach (Enemy e in Enemies)
                    {
                        Random r = new Random();
                        switch (r.Next(0, 5))
                        {
                            case 1:
                                if (Maze[e.CurrentCoords[0] - 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Up(Maze);
                                else if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] - 1] != Cell.Wall)
                                    e.Game_Left(Maze);
                                else if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] + 1] != Cell.Wall)
                                    e.Game_Right(Maze);
                                break;
                            case 2:
                                if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] - 1] != Cell.Wall)
                                    e.Game_Left(Maze);
                                else if (Maze[e.CurrentCoords[0] + 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Down(Maze);
                                else if (Maze[e.CurrentCoords[0] - 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Up(Maze);
                                break;
                            case 3:
                                if (Maze[e.CurrentCoords[0] + 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Down(Maze);
                                else if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] + 1] != Cell.Wall)
                                    e.Game_Right(Maze);
                                else if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] - 1] != Cell.Wall)
                                    e.Game_Left(Maze);
                                break;
                            case 4:
                                if (Maze[e.CurrentCoords[0]][e.CurrentCoords[1] + 1] != Cell.Wall)
                                    e.Game_Right(Maze);
                                else if (Maze[e.CurrentCoords[0] - 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Up(Maze);
                                else if (Maze[e.CurrentCoords[0] + 1][e.CurrentCoords[1]] != Cell.Wall)
                                    e.Game_Down(Maze);
                                break;
                        }
                    }
                }
            }
        }

        private void StartMoveEnemy()
        {
            if (moveEnemy != true)
            {
                MoveEnemies.Start();
                moveEnemy = true;
            }
        }

        private void MovePlayer(int[] coodsExit)
        {
            Show(Maze);
            MoveEnemies = new Thread(MoveEnemy);

            int[] coodsEntry = Maze.CoordinatesOfOne(Cell.Entry);
            Maze[coodsEntry[0]][coodsEntry[1]] = Cell.Player;
            while (InGame)
            {
                KeyEventArgs key = KeyPressed as KeyEventArgs;
                do
                {
                    int[] cur = Maze.CoordinatesOfOne(Cell.Player);//current coords player
                    switch (key.Key)
                    {
                        case Key.Up:
                            UpPlayer(cur, Maze);
                            Show(Maze);
                            StartMoveEnemy();
                            break;
                        case Key.Down:
                            DownPlayer(cur, Maze);
                            Show(Maze);
                            StartMoveEnemy();
                            break;
                        case Key.Left:
                            LeftPlayer(cur, Maze);
                            Show(Maze);
                            StartMoveEnemy();
                            break;
                        case Key.Right:
                            RightPlayer(cur, Maze);
                            Show(Maze);
                            StartMoveEnemy();
                            break;
                    }
                    if (Maze[coodsExit[0]][coodsExit[1]] == Cell.Player) InGame = false;
                    Thread.Sleep(150);
                }
                while (key.Key == (KeyPressed as KeyEventArgs).Key && InGame != false);
            }
            moveEnemy = false;
            MoveEnemies.Abort();
        }

        private void CreateEnemies()
        {
            List<int[]> coords = Maze.CoordinatesOfMany(Cell.Enemy);
            Enemies = new List<Enemy>();
            for (int i = 0; i < coords.Count; i++)
                Enemies.Add(new Enemy(this, coords[i]));
        }

        public void Start()
        {
            do
            {
                try
                {
                    Maze = MazeRead.ReadLevel(Level);
                    int[] coodsExit = Maze.CoordinatesOfOne(Cell.Exit);
                    InGame = true;
                    MovePlayer(coodsExit);
                    CompleteLevel();
                    Level++;
                    KeyPressed = null;
                }
                catch
                {
                    EndLevels();
                    break;
                }
            }
            while (Level <= 3);
            EndLevels();
        }


        private void KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressed = e;
        }
    }
}
