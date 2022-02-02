using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Equipment equipment = null;
    [SerializeField] private Inventory inventory = null;
    //[SerializeField] private TextMeshProUGUI itemQuantityText = null;

    public override HotbarItem SlotItem
    {
        get { return ItemSlot.item; }
        set { }
    }

    public ItemSlot ItemSlot => equipment.ItemContainer.GetSlotByIndex(SlotIndex);

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (itemDragHandler == null) { return; }

        if ((itemDragHandler.ItemSlotUI as EquipmentSlot) != null)
        {
            equipment.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            return;
        }
        InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
        if (inventorySlot != null)
        {
            if (inventorySlot.SlotItem is ConsumableItem)
                Debug.Log("CONSUMABLE ITEM");
            if (SlotItem == null)
            {
                equipment.ItemContainer.AddItemAt(inventorySlot.ItemSlot, this.SlotIndex);
                inventory.ItemContainer.RemoveOne(inventorySlot.SlotIndex);
                UpdateSlotUI();
            }
            else
            {
                ItemSlot old = ItemSlot;
                equipment.ItemContainer.AddItemAt(inventorySlot.ItemSlot, this.SlotIndex);
                inventory.ItemContainer.AddItemAt(old, inventorySlot.SlotIndex);
                UpdateSlotUI();
            }
        }
    }

    public override void UpdateSlotUI()
    {
        if (ItemSlot.item == null)
        {
            EnableSlotUI(false);
            return;
        }

        EnableSlotUI(true);
        itemIconImage.sprite = ItemSlot.item.Icon;

    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
    }
}
