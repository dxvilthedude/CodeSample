using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItem : InventoryItem
{
    [Header("Consumable Date")]
    [SerializeField] private string useText = "Does something, maybe?";
    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Rarity.Name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

        return builder.ToString();
    }
}
