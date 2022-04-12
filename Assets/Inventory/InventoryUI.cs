using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    private InventorySlot[] _slots;

    // Start is called before the first frame update
    private void Start()
    {
        _inventory = Inventory.Instance; //singleton
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = this.GetComponentsInChildren<InventorySlot>();
    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI");
        _slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.Items.Count)
            {
                _slots[i].AddItem(_inventory.Items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}