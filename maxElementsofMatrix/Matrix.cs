using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace maxElementsofMatrix
{
    public class Matrix : IMatrix
    {
        private int[,] data;

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            data = new int[Rows, Columns];
        }

        public int GetValue(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }
            return data[row, column];
        }

        public void SetValue(int row, int column, int value)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }
            data[row, column] = value;
        }

        public int[] FindMaxElements()
        {
            int[] maxElements = new int[Rows];

            for (int i = 0; i < Rows; i++)
            {
                int maxElement = data[i, 0];

                for (int j = 1; j < Columns; j++)
                {
                    if (data[i, j] > maxElement)
                    {
                        maxElement = data[i, j];
                    }
                }

                maxElements[i] = maxElement;
            }

            return maxElements;
        }

        public int[] FindMaxElementsThreads(int numThreads)
        {
            int[] maxElements = new int[Rows];

            Parallel.For(0, Rows, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, row =>
            {
                int maxElement = data[row, 0];

                for (int j = 1; j < Columns; j++)
                {
                    if (data[row, j] > maxElement)
                    {
                        maxElement = data[row, j];
                    }
                }

                maxElements[row] = maxElement;
            });

            return maxElements;
        }

        public int this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }
                return data[row, column];
            }
            set
            {
                if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }
                data[row, column] = value;
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write($"{data[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
