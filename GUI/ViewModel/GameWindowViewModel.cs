using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GUI.BLL;
using GUI.BLL.Interfaces;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI.ViewModel
{

    public class GameWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string mazeString;

        public event PropertyChangedEventHandler PropertyChanged;

        public IGame Game { get; set; }

        public GameWindowViewModel()
        {

            Game = new Game();
            Game.Show += Game_Show;
            Game.CompleteLevel += Game_CompleteLevel;
            Game.EndLevels += Game_EndLevels;
            Game.RestartLevel += Game_RestartLevel;
        }

        private void Game_RestartLevel()
        {
            MessageBox.Show("You Died. (Press \"Ok\" to restart.)", "Died", MessageBoxButton.OK);
        }

        private void Game_EndLevels()
        {
            MessageBox.Show("Levels are over.\nScore: " + Game.Player.Score, "Completed!", MessageBoxButton.OK);
            Environment.Exit(0);
        }

        private void Game_CompleteLevel()
        {
            MessageBox.Show("Levels Completed. (Press \"Ok\" to go next.)", "Completed!", MessageBoxButton.OK);
        }

        private void Game_Show(Cell[][] Maze)
        {
            mazeString = null;

            for (int i = 0; i < Maze.Length; i++)
            {
                for (int j = 0; j < Maze[i].Length; j++)
                {
                    switch (Maze[i][j])
                    {
                        case Cell.Empty:
                            mazeString += "  ";
                            break;
                        case Cell.Wall:
                            mazeString += "▌";
                            break;
                        case Cell.Enemy:
                            mazeString += "x";
                            break;
                        case Cell.Exit:
                            mazeString += "⌂";
                            break;
                        case Cell.Coin:
                            mazeString += "•";
                            break;
                        case Cell.Entry:
                            mazeString += "♦";
                            break;
                        case Cell.Player:
                            switch (Game.KeyPressed)
                            {
                                case Key.Up:
                                    mazeString += "▲";
                                    break;
                                case Key.Down:
                                    mazeString += "▼";
                                    break;
                                case Key.Left:
                                    mazeString += "◄";
                                    break;
                                case Key.Right:
                                    mazeString += "►";
                                    break;
                            }
                            break;
                    }
                }
                mazeString += "\n";
            }
            PropertyChanged(this, new PropertyChangedEventArgs("MazeString"));
        }

        public string MazeString
        {
            get
            {
                if (mazeString == null)
                    Game_Show(Game.Maze);
                return mazeString;
            }
        }


        RelayCommand _startGame;
        public ICommand StartGame
        {
            get
            {
                if (_startGame == null)
                    _startGame = new RelayCommand(() => Game.StartAsync());
                return _startGame;
            }

        }

        public ActionCommand<KeyEventArgs> KeyCommand { get { return new ActionCommand<KeyEventArgs>(OnKeyDown); } }

        private void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key != Key.System)
                Game.KeyPressed = e.Key;
        }
    }
}
