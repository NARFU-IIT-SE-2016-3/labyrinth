using UnityEngine;

/// <summary>
/// Utility methods and helpers
/// </summary>
public static class Utils
{
    /// <summary>
    /// Check if given chance (from 0 to 100%) is succesfull
    /// </summary>
    /// <param name="chance"></param>
    /// <returns></returns>
    public static bool TestChance(float chance)
    {
        var actualChance = Random.Range(0, 100);
        return actualChance <= chance;
    }

    /// <summary>
    /// Get random array element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T PickItem<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static T PickItem2<T>(T[,] array)
    {
        return array[Random.Range(0, array.GetLength(0)), Random.Range(0, array.GetLength(1))];
    }

    /// <summary>
    /// Generate random int number
    /// </summary>
    /// <param name="min">min value (inclusive)</param>
    /// <param name="max">max value (inclusive)</param>
    /// <returns></returns>
    public static int RandInt(int min, int max)
    {
        return min + Random.Range(0, 1) * max;
    }
}
