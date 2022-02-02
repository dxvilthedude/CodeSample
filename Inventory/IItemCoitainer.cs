public interface IItemCoitainer
{
    ItemSlot AddItem(ItemSlot itemSlot);
    ItemSlot AddItemAt(ItemSlot itemSlot,int index);
    void RemoveItem(ItemSlot itemSlot);
    void RemoveAt(int slotIndex);
    void Swap(int indexOne, int indexTwo);
    bool HasItem(InventoryItem item);
    int GetTotalQuantity(InventoryItem item);
}
