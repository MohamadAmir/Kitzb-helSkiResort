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
        public class location
        {
            public int x;
            public int y;
            public int height;
            public bool visited;
            public location(int X,int Y,int Height)
            {
                this.x = X;
                this.y = Y;
                this.height = Height;
                visited = false;
            }
        }
        public class paths
        {
            public Stack<location> path = new Stack<location>();
        }

        private static location[,] map; // matrix data is stored here
        private static int row; // the row length of the matrix
        private static int column; // the column length of the matrix
        
        
        public static paths getLongestPath(int i, int j)
        {
            map[i, j].visited = true;
            paths p = new paths();
            paths curPath = new paths();
            if (j > 0 && map[i,j].height > map[i,j - 1].height)// search left direction
            {
                curPath = getLongestPath(i, j - 1);
                if (curPath.path.Count> p.path.Count) // if the current path length is bigger
                    p = curPath;
            }
            if (j < (column - 1) && map[i,j].height > map[i,j + 1].height)// search right direction
            {
                curPath = getLongestPath(i, j + 1);
                if (curPath.path.Count > p.path.Count)
                    p = curPath;
            }
            if (i > 0 && map[i,j].height > map[i - 1,j].height)// search Up direction
            {
                curPath = getLongestPath(i - 1, j);
                if (curPath.path.Count > p.path.Count)
                    p = curPath;
            }
            if (i < (row - 1) && map[i,j].height > map[i + 1,j].height)// search down direction
            {
                curPath = getLongestPath(i + 1, j);
                if (curPath.path.Count > p.path.Count)
                {
                    p = curPath;       
                }
            }
            location l = new location(i,j, map[i, j].height);
            p.path.Push(l);
            return p;
        }

        public static void loadMapFromFile(string filePath)//load file data to Map
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                column = Convert.ToInt32(lines[0].Split(' ')[0]);
                row = Convert.ToInt32(lines[0].Split(' ')[1]);

                map = new location[column, row];
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] mapVerticalLocations = lines[i].Split(' ');
                    for (int j = 0; j < mapVerticalLocations.Length; j++)
                    {
                        if (mapVerticalLocations[j] != null)
                        {
                            try
                            {
                                map[i - 1, j] = new location(i,j, Convert.ToInt32(mapVerticalLocations[j]));
                                
                            }
                            catch
                            {
                                map[i - 1, j] = new location(i, j, -1);
                            }
                        }
                    }
                }
            }
            catch (Exception e2)
            { Console.WriteLine("An error occurred while loading file: " + e2.Message); }
        }
        static void Main(string[] args)
        {
            loadMapFromFile(@"maps\map.txt");
            paths longestPath = new paths();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (!map[i,j].visited)
                    {
                        paths temp = getLongestPath(i, j);
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
            }
            Console.WriteLine("Maximal Path is: " + longestPath.path.Count.ToString()+ "\nMaximal Drop is: " + (longestPath.path.Peek().height-longestPath.path.ElementAt(longestPath.path.Count-1).height).ToString());
            Console.WriteLine("The path is : ");
            string printPath = "";
            for (int i = 0; i < longestPath.path.Count; i++)
            {
                if (i == longestPath.path.Count - 1)
                {
                    printPath += longestPath.path.ElementAt(i).height.ToString();
                }
                else
                {
                    printPath += longestPath.path.ElementAt(i).height.ToString() + "  >==>  ";
                }
            }
            Console.WriteLine(printPath);
            System.Console.ReadKey();
        }
    }
}
