using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private Equipment equipment = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    public override HotbarItem SlotItem 
    {
        get { return ItemSlot.item; }
        set { }
    }

    public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (itemDragHandler == null) { return; }

        if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
        {
            inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            return;
        }
        EquipmentSlot equipmentSlot = itemDragHandler.ItemSlotUI as EquipmentSlot;
        if (equipmentSlot != null)
        {
            if (SlotItem == null)
            {
                inventory.ItemContainer.AddItemAt(equipmentSlot.ItemSlot, this.SlotIndex);
                equipment.ItemContainer.RemoveAt(equipmentSlot.SlotIndex);
                UpdateSlotUI();
            }
            else
            {
                ItemSlot old = ItemSlot;
                inventory.ItemContainer.AddItemAt(equipmentSlot.ItemSlot, this.SlotIndex);
                equipment.ItemContainer.AddItemAt(old, equipmentSlot.SlotIndex);
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
        itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }
}
