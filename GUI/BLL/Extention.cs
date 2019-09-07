using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.BLL
{
    ///<summary>
    ///Extention is needed for find coordinates of cells.
    ///</summary>
    public static class Extention
    {
        ///<summary>
        ///Find coordinates of one cell.
        ///</summary>
        public static int[] CoordinatesOfOne<T>(this T[][] matrix, T value)
        {
            int h = matrix.Length - 1;
            int w = matrix[0].Length - 1;

            int[] coords = new int[2];
            coords[0] = -1;
            coords[1] = -1;

            for (int x = 0; x < h; ++x)
            {
                for (int y = 0; y < w; ++y)
                {
                    if (matrix[x][y].Equals(value))
                    {
                        coords[0] = x;
                        coords[1] = y;
                        return coords;
                    }
                }
            }
            return coords;
        }

        ///<summary>
        ///Find coordinates of many cells.
        ///</summary>
        public static List<int[]> CoordinatesOfMany<T>(this T[][] matrix, T value)
        {
            List<int[]> coords = new List<int[]>();
            int h = matrix.Length - 1;
            int w = matrix[0].Length - 1;

            int[] coord = new int[2];
            coord[0] = -1;
            coord[1] = -1;

            for (int x = 0; x < h; ++x)
            {
                for (int y = 0; y < w; ++y)
                {
                    if (matrix[x][y].Equals(value))
                    {
                        coord[0] = x;
                        coord[1] = y;
                        coords.Add(coord);
                        coord = new int[2];
                    }
                }
            }
            return coords;
        }
    }
}
