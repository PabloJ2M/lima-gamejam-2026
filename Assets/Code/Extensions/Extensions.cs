using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }

    public static void InterleaveShuffle<T>(this List<T> list)
    {
        var temp = new List<T>(list.Count);
        int mid = (list.Count + 1) / 2;

        for (int i = 0; i < mid; i++)
        {
            temp.Add(list[i]);
            if (i + mid < list.Count)
                temp.Add(list[i + mid]);
        }

        list.Clear();
        list.AddRange(temp);
    }
}