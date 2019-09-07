using GUI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.BLL
{
    ///<summary>
    ///Class is needed to Read .txt files.
    ///</summary>
    public class MazeRead
    {
        ///<summary>
        ///Read .txt file, and return Maze level.
        ///</summary>
        public static Cell[][] ReadLevel(int levelDig)
        {
            string str = Directory.GetCurrentDirectory().Replace("GUI\\bin\\Debug", string.Format("BLL\\Levels\\Level{0}.txt", levelDig.ToString()));
            using (StreamReader sr = new StreamReader(str))
            {
                Cell[][] Maze;
                int rows = 0;
                List<string> lines = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null) lines.Add(line);
                Maze = new Cell[lines.Count][];
                foreach (string text in lines)
                {
                    char[] cells = text.ToCharArray();
                    Maze[rows] = new Cell[cells.Length];

                    for (int i = 0; i < cells.Length; i++)
                    {
                        switch (cells[i])
                        {
                            case '0':
                                Maze[rows][i] = Cell.Empty;
                                break;
                            case '1':
                                Maze[rows][i] = Cell.Wall;
                                break;
                            case '2':
                                Maze[rows][i] = Cell.Coin;
                                break;
                            case '4':
                                Maze[rows][i] = Cell.Entry;
                                break;
                            case '5':
                                Maze[rows][i] = Cell.Enemy;
                                break;
                            case '6':
                                Maze[rows][i] = Cell.Exit;
                                break;
                            default:
                                break;

                        }

                    }
                    rows++;
                }
                return Maze;
            }
        }
    }
}
