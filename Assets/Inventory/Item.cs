using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")] //create new menu in unity create
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}