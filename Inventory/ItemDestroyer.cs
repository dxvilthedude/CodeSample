using TMPro;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private Equipment equipment = null;
    [SerializeField] private TextMeshProUGUI destroyText;
    private int slotIndex = 0;
    private bool isInventory = false;
    private void OnDisable() => slotIndex = -1;
    public void Activate(ItemSlot itemSlot, int slotIndex, bool isInventory)
    {
        this.slotIndex = slotIndex;
        destroyText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColouredName}?";

        gameObject.SetActive(true);
        this.isInventory = isInventory;
    }

    public void Destroy()
    {
        if (isInventory)
            inventory.ItemContainer.RemoveAt(slotIndex);
        else
            equipment.ItemContainer.RemoveAt(slotIndex);

        gameObject.SetActive(false);
    }
}
