using System;

namespace XMixDrix
{
    public struct Cell
    {
        public int m_Row;
        public int m_Col;

        public Cell(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public static void PrintCell(Cell i_Cell)
        {
            Console.WriteLine(i_Cell.m_Row + "," + i_Cell.m_Col);
        }
    }
}