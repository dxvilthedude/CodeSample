using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Equipment : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
    public ItemContainer ItemContainer { get; } = new ItemContainer(10);

    public void OnEnable() => ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;

    public void OnDisable() => ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
}
