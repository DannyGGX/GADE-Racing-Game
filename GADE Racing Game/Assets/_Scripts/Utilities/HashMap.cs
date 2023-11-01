using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for a Hashtable
/// </summary>
/// <typeparam name="Key"></typeparam>
/// <typeparam name="Value"></typeparam>
public class HashMap<Key, Value> where Key : Object where Value : Object
{
    private Hashtable hashTable;

    public HashMap()
    {
        hashTable = new Hashtable();
    }

    public void Add(Key key, Value value)
    {
        hashTable.Add(key, value);
    }

    public Value GetValue(Key key)
    {
        return (Value)hashTable[key];
    }

    public bool ContainsValue(Value value)
    {
        return hashTable.ContainsValue(value);
    }
}
