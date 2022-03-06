using System;
using UnityEngine;

public static class Levenshtein
{
    /// SOURCE : https://www.developpez.net/forums/d644186/dotnet/general-dotnet/contribuez/algorithme-evaluer-similarite-entre-2-chaines/
    /// <summary>
    /// Computes the Levenshtein distance between 2 strings
    /// Based on the algorithm described here :
    /// http://en.wikipedia.org/wiki/Levenshtein_distance
    /// </summary>
    /// <param name="a">first string to compare</param>
    /// <param name="b">second string to compare</param>
    /// <param name="caseSensitive">true if case should be considered, false otherwise</param>
    /// <returns>the Levenshtein distance between the 2 strings</returns>
    private static int ComputeDistance(string a, string b, bool caseSensitive)
    {
        if (!caseSensitive)
        {
            a = a.ToLower();
            b = b.ToLower();
        }

        int m = a.Length;
        int n = b.Length;
        int[,] d = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++)
            d[i, 0] = i;
        for (int i = 0; i <= n; i++)
            d[0, i] = i;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                int cost;
                if (a[i - 1] == b[j - 1])
                    cost = 0;
                else
                    cost = 1;
                d[i, j] = Min(d[i - 1, j] + 1,
                                d[i, j - 1] + 1,
                                d[i - 1, j - 1] + cost);
            }
        }

        return d[m, n];
    }

    /// <summary>
    /// Computes the correlation coefficient between 2 strings, based on the Levenshtein distance
    /// </summary>
    /// <param name="a">first string to compare</param>
    /// <param name="b">second string to compare</param>
    /// <param name="caseSensitive">true if case should be considered, false otherwise</param>
    /// <returns>The correlation coefficient between the 2 strings. This value can range from 0 (completely different strings) to 1 (identical strings)</returns>
    public static double ComputeCorrelation(string a, string b, bool caseSensitive)
    {
        int distance = ComputeDistance(a, b, caseSensitive);
        int longest = Max(a.Length, b.Length);
        double coef = 1 - (double)distance / longest;
        return coef;
    }

    private static T Min<T>(T arg0, params T[] args) where T : IComparable
    {
        T min = arg0;
        for (int i = 0; i < args.Length; i++)
        {
            T x = args[i];
            if (x.CompareTo(min) < 0)
                min = x;
        }
        return min;
    }

    private static T Max<T>(T arg0, params T[] args) where T : IComparable
    {
        T max = arg0;
        for (int i = 0; i < args.Length; i++)
        {
            T x = args[i];
            if (x.CompareTo(max) > 0)
                max = x;
        }
        return max;
    }
}

