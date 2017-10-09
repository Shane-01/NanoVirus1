using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NanoVirus
{
    class Method
    {

        public static List<Cell> GenerateCells()
        {
            List<Cell> list = new List<Cell>();
            Random rnd2 = new Random();
            Random rnd3 = new Random();

            for (int i = 0; i < 100; i++)
            {

                int x = rnd3.Next(1, 5001);
                int y = rnd3.Next(1, 5001);
                int z = rnd3.Next(1, 5001);

                int cells = rnd2.Next(1, 101);
                if (cells <= 5)
                {
                    list.Add(new Cell(x, y, z, "Tumorous"));
                }
                else if (cells > 5 && cells <= 30)
                {
                    list.Add(new Cell(x, y, z, "White Blood"));
                }
                else if (cells > 30 && cells <= 100)
                {
                    list.Add(new Cell(x, y, z, "Red Blood"));
                }
            }
            return list;

        }
        public static List<NanoVirus> Start(List<Cell> list)
        {
            List<NanoVirus> n = new List<NanoVirus>();


            int objCount = 0;
            Random rnd1 = new Random();

            objCount = list.Count;

            int arrysize = list.Count(a => a.CellType == "Red Blood");

            List<int> RedBloodIndex1 = new List<int>();

            for (int i = 0; i < arrysize; i++)
            {
                if (list[i].CellType == "Red Blood")
                {
                    RedBloodIndex1.Add(i);
                }
            }

            int NanoStartIndexPossibilities = rnd1.Next(0, RedBloodIndex1.Count);
            int NanoStartIndex = RedBloodIndex1[NanoStartIndexPossibilities];

            n.Add(new NanoVirus(int.Parse(list[NanoStartIndex].X.ToString()),
                int.Parse(list[NanoStartIndex].Y.ToString()),
                int.Parse(list[NanoStartIndex].Z.ToString()),
                list[NanoStartIndex].CellType.ToString()));

            return n;

        }

        public static List<NanoVirus> MoveAfterDestroyed(List<Cell> list)
        {
            List<NanoVirus> n = new List<NanoVirus>();
            int objCount = 0;
            Random rnd1 = new Random();
            objCount = list.Count;
            List<int> MoveIndex = new List<int>();

            for (int i = 0; i < objCount; i++)
            {
                MoveIndex.Add(i);
            }

            int NanoStartIndexPossibilities = rnd1.Next(0, MoveIndex.Count);
            int NanoStartIndex = MoveIndex[NanoStartIndexPossibilities];


            n.Add(new NanoVirus(int.Parse(list[NanoStartIndex].X.ToString()),
                int.Parse(list[NanoStartIndex].Y.ToString()),
                int.Parse(list[NanoStartIndex].Z.ToString()),
                list[NanoStartIndex].CellType.ToString()));

            return n;

        }



        public static Dictionary<int, int> CalculateDistance(List<NanoVirus> list, List<Cell> list2)
        {

            Dictionary<int, int> distances = new Dictionary<int, int>();
            foreach (var item in list)
            {


                for (int i = 0; i < list2.Count; i++)
                {

                    int s =
                        Convert.ToInt32(Math.Abs((Math.Pow(Convert.ToDouble((list2[i].X - item.X)), 2.0))
                        + (Math.Pow(Convert.ToDouble((list2[i].Y - item.Y)), 2.0))
                        + (Math.Pow(Convert.ToDouble((list2[i].Z - item.Z)), 2.0))));
                    double d = Math.Sqrt(Convert.ToDouble(s));

                    int distance = Convert.ToInt32(d);
                    distances.Add(i, distance);
                }

            }
            var sortedDict = from entry in distances orderby entry.Value ascending select entry;
            return sortedDict.ToDictionary<KeyValuePair<int, int>, int, int>(pair => pair.Key, pair => pair.Value);
        }


        public static Dictionary<List<NanoVirus>, List<Cell>> MoveNanoVirus(List<NanoVirus> list, List<Cell> list2)
        {
            Dictionary<List<NanoVirus>, List<Cell>> Dict = new Dictionary<List<NanoVirus>, List<Cell>>();
            Dictionary<int, int> dist = CalculateDistance(list, list2);
            int possiblemoves = dist.Count(xx => xx.Value <= 5000);

            for (int i = 0; i < possiblemoves; i++)
            {
                Dictionary<List<NanoVirus>, List<Cell>> temp = new Dictionary<List<NanoVirus>, List<Cell>>();

                Random r = new Random();
                int move = r.Next(1, possiblemoves + 1);

                var item = dist.ElementAt(move);
                var itemKey = item.Key;
                var itemValue = item.Value;

                list.Clear();
                list.Add(new NanoVirus(list2[itemKey].X, list2[itemKey].Y, list2[itemKey].Z, list2[itemKey].CellType));

                temp.Add(list, list2);
                Dict = temp;

            }
            return Dict;
        }


        public static int CalculateDistancToInfect(Cell tumour, List<Cell> list2)
        {
            int index = 0;
            int smallestDistance = 10000;

            for (int i = 0; i < list2.Count; i++)
            {

                int s =
                    Convert.ToInt32(Math.Abs((Math.Pow(Convert.ToDouble((list2[i].X - tumour.X)), 2.0)) 
                    + (Math.Pow(Convert.ToDouble((list2[i].Y - tumour.Y)), 2.0))
                    + (Math.Pow(Convert.ToDouble((list2[i].Z - tumour.Z)), 2.0))));
                
                double d = Math.Sqrt(Convert.ToDouble(s));

                int distance = Convert.ToInt32(d);
                if (distance != 0 && distance < smallestDistance)
                {
                    smallestDistance = distance;
                    index = i;
                }

            }
            return index;
        }

        public static void TumorousInfect(List<Cell> list)
        {
            List<Cell> Tum = new List<Cell>();
            List<Cell> Red = new List<Cell>();
            List<Cell> White = new List<Cell>();

            Red = list.Where(o => o.CellType == "Red Blood").ToList();
            White = list.Where(o => o.CellType == "White Blood").ToList();
            Tum = list.Where(o => o.CellType == "Tumorous").ToList();

            int tumorCount = Tum.Count;
            for (int i = 0; i < tumorCount; i++)
            {
                if (Red.Count > 0)
                {
                    int dist = CalculateDistancToInfect(Tum[i], Red);
                    list[list.IndexOf(Red[dist])].CellType = "Tumorous";
                    Tum.Add(Red[dist]);
                    Red.RemoveAt(dist);
                }

                else if (White.Count > 0)
                {
                    int dist = CalculateDistancToInfect(Tum[i], White);
                    list[list.IndexOf(White[dist])].CellType = "Tumorous";
                    Tum.Add(White[dist]);
                    White.RemoveAt(dist);

                }
            }
        }


        public static List<Cell> NanoVirusActiveCycleStart(List<NanoVirus> list, List<Cell> list2)
        {
            int cycles = 1;
            string path = string.Concat( Environment.CurrentDirectory, @"\NanoVirus.txt");
            File.WriteAllText(path, String.Empty);

            for (int q = 0; q < list2.Count; q++)
            {
                Random rnd4 = new Random();
                int option = rnd4.Next(1, 22);

                if (option >= 1 && option < 11)//move
                {
                    Dictionary<List<NanoVirus>, List<Cell>> temp = MoveNanoVirus(list, list2);

                    for (int i = 0; i < temp.Count; i++)
                    {
                        var item = temp.ElementAt(i);
                        var itemKey = item.Key;
                        var itemValue = item.Value;

                        list = itemKey;
                        list2 = itemValue;
                    }

                }
                else if (option > 10 && option < 21)//destroy current cell
                {
                    Cell t = list2.Where(o => o.X == list[0].X && o.Y == list[0].Y && o.Z == list[0].Z && o.CellType == list[0].CellType).SingleOrDefault();
                    list2.Remove(t);
                    list = MoveAfterDestroyed(list2);
                }
                else if (option > 20 && option < 31)//do nothing
                { }
                
                if (cycles % 5 == 0)
                {
                    TumorousInfect(list2);
                }

                int redCount = list2.Count(a => a.CellType == "Red Blood");
                int whiteCount = list2.Count(b => b.CellType == "White Blood");
                int tumCount = list2.Count(c => c.CellType == "Tumorous");
                string NanoCellAT = list[0].CellType;

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine("Cycle:" + cycles);
                    file.WriteLine("Total Cells:" + list2.Count);
                    file.WriteLine("Red Blood Cells:" + redCount);
                    file.WriteLine("White Blood Cells:" + whiteCount);
                    file.WriteLine("Tumorous Cells:" + tumCount);
                    file.WriteLine("Current Cell NanoVirus Is on:" + NanoCellAT);
                    file.WriteLine("");
                }

                int redCheck = list2.Count(a => a.CellType == "Red Blood");
                int whiteCheck = list2.Count(b => b.CellType == "White Blood");

                if (redCheck == 0 && whiteCheck == 0)
                {
                    break;
                }
                cycles++;

            }

            return list2;

        }
    }
}
