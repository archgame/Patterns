using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton Pattern

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("");
            return;
        }
        Instance = this;
    }

    #endregion Singleton Pattern

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    public List<Item> Items = new List<Item>();

    public int Space = 16;

    public bool Add(Item item)
    {
        Debug.Log("Add");
        if (Items.Count >= Space) { Debug.Log("Items Full"); return false; }
        Items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}