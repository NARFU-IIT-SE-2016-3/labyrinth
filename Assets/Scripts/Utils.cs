using UnityEngine;

public static class Utils
{
    public static bool TestChance(float chance)
    {
        var actualChance = Random.Range(0, 100);
        return actualChance <= chance;
    }

    public static T PickItem<T>(params T[] array)
    {
        return array[Random.Range(0, array.Length)];
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
