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
}
