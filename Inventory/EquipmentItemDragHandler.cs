using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItemDragHandler : ItemDragHandler
{
    [SerializeField] private ItemDestroyer itemDestroyer = null;
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);
            if (eventData.hovered.Count == 0)
            {
                EquipmentSlot thisSlot = ItemSlotUI as EquipmentSlot;
                itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex, false);
            }
        }

    }
}

