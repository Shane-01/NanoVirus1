using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class Cell
    {
        private int x;
        private int y;
        private int z;
        private string cellType;
        
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public string CellType
        {
            get
            {
                return cellType;
            }

            set
            {
                cellType = value;
            }
        }

        public Cell(int x,int y,int z, string cellType)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.cellType = cellType;
        }

        public override string ToString()
        {
            return String.Format("X:{0} Y:{1} Z:{2} CellType:{3}",x,y,z,cellType);
        }
        
    }
}
