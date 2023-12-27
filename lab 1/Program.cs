using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        // Чтение входных данных из файла
        string[] inputLines = File.ReadAllLines("INPUT.txt");
        string[] input = inputLines[0].Split();
        int N = int.Parse(input[0]);
        int M = int.Parse(input[1]);

        char[,] table = new char[N, N];

        // Заполнение таблицы
        for (int i = 0; i < N; i++)
        {
            string row = inputLines[i + 1];
            for (int j = 0; j < N; j++)
            {
                table[i, j] = row[j];
            }
        }

        List<string> keywords = new List<string>();

        // Чтение ключевых слов
        for (int i = N + 1; i < N + M + 1; i++)
        {
            keywords.Add(inputLines[i]);
        }

        // Поиск и вычеркивание ключевых слов
        foreach (string keyword in keywords)
        {
            MarkWordInTable(table, keyword);
        }

        // Поиск оставшихся букв
        List<char> remainingLetters = FindRemainingLetters(table);

        // Сортировка оставшихся букв в алфавитном порядке
        remainingLetters.Sort();

        // Запись результата в файл OUTPUT.TXT
        File.WriteAllLines("OUTPUT.txt", new[] { string.Join("", remainingLetters) });
    }

    static void MarkWordInTable(char[,] table, string word)
    {
        int N = table.GetLength(0);
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (table[i, j] == word[0])
                {
                    // Поиск слова в таблице
                    char[,] table2 = CopyMatrix(table);
                    table = CheckWordInTable(table, word, i, j);
                    bool areEqual = AreMatricesEqual(table, table2);
                    if (!areEqual)
                    {
                        return;
                    }
                    else {
                        table = table2;
                    }
                }
            }
        }
    }
    static char[,] CopyMatrix(char[,] original)
    {
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);
        char[,] copy = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = original[i, j];
            }
        }

        return copy;
    }
    static bool AreMatricesEqual(char[,] matrix1, char[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
        {
            return false; // Размеры матриц различны
        }

        for (int i = 0; i < matrix1.GetLength(0); i++)
        {
            for (int j = 0; j < matrix1.GetLength(1); j++)
            {
                if (matrix1[i, j] != matrix2[i, j])
                {
                    return false; // Элементы матриц различны
                }
            }
        }

        return true; // Матрицы идентичны
    }
    static char[,] CheckWordInTable(char[,] table, string word, int row, int col)
    {
        int N = table.GetLength(0);
        int k = 0;
        char[,] table1 = table;
        table[row,col] = '\0';
        // Проверка слова
        try
        {
            for (int i = 1; i<word.Length; i++)
            {

                if (table[row, col + 1] == word[i])
                {
                    table[row, col + 1] = '\0';
                    col = col + 1;
                }
                else if (table[row + 1, col] == word[i])
                {
                    table[row + 1, col] = '\0';
                    row = row + 1;
                }
                else if (row-1>=0 && table[row - 1, col] == word[i])
                        {
                            table[row - 1, col] = '\0';
                            row = row - 1;
                        }
                else if (col-1>=0 && table[row, col - 1] == word[i])
                        {
                            table[row, col - 1] = '\0';
                            col = col - 1;
                        }
                else {
                    return table1;
                }
                
            }
            return table;
        }
        catch (Exception x) 
        {

            return table1;
        }
        
    }

    static List<char> FindRemainingLetters(char[,] table)
    {
        int N = table.GetLength(0);
        List<char> remainingLetters = new List<char>();

        // Собираем оставшиеся буквы в список
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (table[i, j] != '\0')
                {
                    remainingLetters.Add(table[i, j]);
                }
            }
        }

        return remainingLetters;
    }
}
