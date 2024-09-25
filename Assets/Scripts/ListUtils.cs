using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListUtils 
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomNum = Random.Range(0, list.Count);
            T tmp = list[randomNum];
            list[randomNum] = list[i];
            list[i] = tmp;

        }
    }
}
