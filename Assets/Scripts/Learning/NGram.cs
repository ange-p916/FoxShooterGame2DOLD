using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class KeyData<T>
{
    public Dictionary<T, int> counts;
    public int total;

    public KeyData()
    {
        counts = new Dictionary<T, int>();
    }


}


public class NGram<T>
{
    Dictionary<string, KeyData<T>> data;
    public int nValue;

    public NGram(int windowSize)
    {
        nValue = windowSize + 1;
        data = new Dictionary<string, KeyData<T>>();
    }

    public static string ArrToStrKey(ref T[] actions)
    {
        var builder = new StringBuilder();
        foreach (T k in actions)
        {
            builder.Append(k.ToString());
        }

        return builder.ToString();
    }

    public void RegisterSequence(T[] actions)
    {
        string key = ArrToStrKey(ref actions);
        var val = actions[nValue - 1];
        if(!data.ContainsKey(key))
        {
            data[key] = new KeyData<T>();
            KeyData<T> kdr = data[key];
            if(kdr.counts.ContainsKey(val))
            {
                kdr.counts[val] = 0;
            }
            kdr.counts[val]++;
            kdr.total++;
        }
    }

    public T GetMostLikely(T[] actions)
    {
        string key = ArrToStrKey(ref actions);
        KeyData<T> kdr = data[key];
        int highestVal = 0;
        T bestAction = default(T);
        foreach (KeyValuePair<T, int> kvp in kdr.counts)
        {
            if(kvp.Value > highestVal)
            {
                bestAction = kvp.Key;
                highestVal = kvp.Value;
            }
        }

        return bestAction;
    }
	
}
