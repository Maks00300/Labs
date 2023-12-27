using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Чтение входных данных из файла
        string[] inputLines = File.ReadAllLines("INPUT.txt");

        // Парсинг входных данных
        int[] input1 = inputLines[0].Split().Select(int.Parse).ToArray();
        int N = input1[0];
        int K = input1[1];
        int T = input1[2];

        int[] T_values = inputLines[1].Split().Select(int.Parse).ToArray();
        int[] P_values = inputLines[2].Split().Select(int.Parse).ToArray();
        int[] S_values = inputLines[3].Split().Select(int.Parse).ToArray();

        // Инициализация массива для хранения максимального богатства
        int[][] dp = new int[N + 1][];
        for (int i = 0; i <= N; i++)
        {
            dp[i] = new int[K + 1];
        }

        // Заполнение массива dp
        for (int i = 1; i <= N; i++)
        {
            for (int j = 0; j <= K; j++)
            {
                dp[i][j] = dp[i - 1][j];
                if (j >= S_values[i - 1])
                {
                    dp[i][j] = Math.Max(dp[i][j], dp[i - 1][j - S_values[i - 1]] + P_values[i - 1]);
                }
            }
        }

        // Вывод результата в файл
        File.WriteAllText("OUTPUT.txt", dp[N][K].ToString());
    }
}
