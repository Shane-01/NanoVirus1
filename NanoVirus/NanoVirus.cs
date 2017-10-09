using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NanoVirus
{
    class NanoVirus : Cell
    {
        public NanoVirus(int x, int y, int z, string cellType) : base(x, y, z, cellType)
        {
        }

        public override string ToString()
        {
            return String.Format("X:{0} Y:{1} Z:{2} CellType:{3}", X, Y, Z, CellType);
        }
        

    }
}
