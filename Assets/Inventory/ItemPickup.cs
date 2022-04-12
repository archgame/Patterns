using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        //Debug.Log("Pick Up: " + item.name);
        //add to inventory
        bool pickedUp = Inventory.Instance.Add(item);
        if (pickedUp) { Destroy(gameObject); }
    }
}