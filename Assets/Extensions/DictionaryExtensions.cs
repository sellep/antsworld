using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class DictionaryExtensions
{

    public static void MaxOrDefault<K, V>(this Dictionary<K, V> dict, out K k, out V v) where K : class where V : IComparable
    {
        k = null;
        v = default(V);

        foreach (KeyValuePair<K, V> pair in dict)
        {
            if (k == null || v.CompareTo(pair.Value) == -1)
            {
                k = pair.Key;
                v = pair.Value;
            }
        }
    }

    public static void SafelyIncrement<K>(this Dictionary<K, int> dict, K k)
    {
        if (dict.ContainsKey(k))
        {
            dict[k]++;
        }
        else
        {
            dict.Add(k, 1);
        }
    }

    public static void SafelyDecrement<K>(this Dictionary<K, int> dict, K k)
    {
        dict[k]--;
    }
}