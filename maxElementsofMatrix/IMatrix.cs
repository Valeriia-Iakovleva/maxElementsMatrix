using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maxElementsofMatrix
{
    public interface IMatrix
    {
        int Rows { get; }
        int Columns { get; }

        int GetValue(int row, int column);
        void SetValue(int row, int column, int value);
        int[] FindMaxElements();
    }
}
