using UnityEngine;

public static class Parser
{
    private const int BLOCK_SIZE = 64;

    public static void SerializeInts(ref string res, params int[] ints)
    {
        for (int i = 0; i < ints.Length; i++)
        {
            res += ints[i].ToString("X64");
        }
    }

    public static int DeserializeInt(string res)
    {
        return int.Parse(res, System.Globalization.NumberStyles.HexNumber);
    }

    public static int[,] DeserializeIntMatrix(string res, int col)
    {
        int length = int.Parse(res.Substring(BLOCK_SIZE, BLOCK_SIZE), System.Globalization.NumberStyles.HexNumber);

        int[,] matrix = new int[col, length];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < col; j++)
            {
                string str = res.Substring((i * col + j + 2) * BLOCK_SIZE + 56, 8);
                matrix[j, i] = int.Parse(str, System.Globalization.NumberStyles.HexNumber);
            }
        }

        return matrix;
    }
}
