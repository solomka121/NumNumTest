using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    [System.Serializable]
    public class BoardRow
    {
        public int Size;
        public int[] Row;

        public BoardRow()
        {
        }

        public BoardRow(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            Size = size;
            Row = new int [Size];
        }

        public void ClearRow()
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = -1;
            }
        }
    }

    public int Columns = 0;
    public int Rows = 0;

    public BoardRow[] Board;

    public void ClearWithEmptyString()
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }

        ClearWithEmptyString();
    }
}