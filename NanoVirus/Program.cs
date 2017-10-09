using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class Program
    {
        static void Main(string[] args)
        {
           
            List<NanoVirus> NanoVirus = new List<NanoVirus>();
            List<Cell> Cells = new List<Cell>();
            Cells = Method.GenerateCells();
            NanoVirus = Method.Start(Cells);

            Cells = Method.NanoVirusActiveCycleStart(NanoVirus, Cells);

            if (Cells.Count == 0)
            {
                Console.WriteLine("All Cells Destroyed");
                
            }
            else if (Cells.Count == Cells.Count(a => a.CellType == "Tumorous"))
            {
                Console.WriteLine("You Loose");
                Console.WriteLine(String.Format("Only Tumorous Cells remain Count:{0}", Cells.Count(a => a.CellType == "Tumorous")));
              
            }
            else if(Cells.Count(a => a.CellType == "Tumorous")==0)
            {
                Console.WriteLine("You've Won");
            }


            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
