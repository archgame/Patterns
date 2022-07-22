using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ginventory
{
    private List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject i)
    {
        items.Add(i);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject i in items)
        {
            if (i.tag == tag)
            {
                return i;
            }
        }
        return null; //item is not in inventory
    }

    public void RemoveItem(GameObject i)
    {
        int indexToRemove = -1;
        foreach (GameObject g in items)
        {
            indexToRemove++;
            if (g == i)
                break;
        }
        if (indexToRemove >= -1)
            items.RemoveAt(indexToRemove);
    }
}