using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        private static location[,] map; // matrix data is stored here
        private static int row; // the row length of the matrix
        private static int column; // the column length of the matrix
        public class location { public int x; public int y; public int height; }
        public class pathes { public int[] pathLength=new int[3];public Stack<location> path = new Stack<location>(); } 
        public static pathes getLongPath(int i, int j)
        {
            pathes p = new pathes();
            pathes curPath = new pathes();
            if (j > 0 && map[i,j].height > map[i,j - 1].height)// search left direction
            {
                curPath = getLongPath(i, j - 1);
                if (curPath.pathLength[0] > p.pathLength[0]) // if the current path length is bigger
                    p = curPath;
            }
            if (j < (column - 1) && map[i,j].height > map[i,j + 1].height)// search right direction
            {
                curPath = getLongPath(i, j + 1);
                if (curPath.pathLength[0] > p.pathLength[0])
                    p = curPath;
            }
            // search from the left direction
            if (i > 0 && map[i,j].height > map[i - 1,j].height)// search Up direction
            {
                curPath = getLongPath(i - 1, j);
                if (curPath.pathLength[0] > p.pathLength[0])
                    p = curPath;
            }
            if (i < (row - 1) && map[i,j].height > map[i + 1,j].height)// search down direction
            {
                curPath = getLongPath(i + 1, j);
                if (curPath.pathLength[0] > p.pathLength[0])
                {
                    p = curPath;
                    
                }
            }
            p.pathLength[0]++;// add 1 to the path length
            location l = new location();
            l.x = i;
            l.y = j;
            l.height = map[i, j].height;
            p.path.Push(l);
            return p;
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"d:\map.txt");
            column = Convert.ToInt32(lines[0].Split(' ')[0]);
            row = Convert.ToInt32(lines[0].Split(' ')[1]);
            
            map = new location[column,row];
            for (int i = 1; i < lines.Length; i++)
            {
                string[] mapVerticalLocations = lines[i].Split(' ');
                for (int j = 0; j < mapVerticalLocations.Length; j++)
                {
                    if (mapVerticalLocations[j] != null)
                    {
                        try
                        {
                            map[i - 1, j] = new location();
                            map[i - 1, j].height = Convert.ToInt32(mapVerticalLocations[j]);
                            map[i - 1, j].x = i;
                            map[i - 1, j].y = j;
                        }
                        catch 
                        {
                            map[i - 1, j].height = -1;
                            map[i - 1, j].x = i;
                            map[i - 1, j].y = j;
                        }
                    }
                }
            }
            /*//////////////////////////////*/
            pathes longestPath = new pathes();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {

                    pathes temp = getLongPath(i, j);
                    if (longestPath.path.Count == temp.path.Count)
                    {
                        if (longestPath.path.Count > 0)
                        {
                            if ((temp.path.Peek().height - temp.path.ElementAt(temp.path.Count - 1).height) > (longestPath.path.Peek().height - longestPath.path.ElementAt(longestPath.path.Count - 1).height))
                            {
                                longestPath = temp;
                            }
                        }
                        else
                        {
                            longestPath = temp;
                        }
                    }
                    else if (longestPath.path.Count < temp.path.Count)
                    {
                        longestPath = temp;
                    }
                }
            }
            Console.WriteLine("Maximal Path is: " + longestPath.pathLength[0]+ "\nMaximal Drop is: " + (longestPath.path.Peek().height-longestPath.path.ElementAt(longestPath.path.Count-1).height).ToString());
            Console.WriteLine("The path is :\n");
            for (int i = 0; i < longestPath.path.Count; i++)
            {
                Console.WriteLine(longestPath.path.ElementAt(i).height.ToString()+ "  >==>  ");
            }
            System.Console.ReadKey();
        }
    }
}
