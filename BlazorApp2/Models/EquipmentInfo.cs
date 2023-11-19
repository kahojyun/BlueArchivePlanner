using System.Text.Json.Serialization;

namespace BlazorApp2.Models;

public class EquipmentInfo
{
    public int Id { get; set; }
    public string? Category { get; set; }
    public string? Rarity { get; set; }
    public int Tier { get; set; }
    public string? Icon { get; set; }
    public Shop[]? Shops { get; set; }
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public bool[]? IsReleased { get; set; }
    public string[]? StatType { get; set; }
    public int[][]? StatValue { get; set; }
    public int[][]? Recipe { get; set; }
    public int RecipeCost { get; set; }
}

public class Shop
{
    public string? ShopCategory { get; set; }
    public bool[]? Released { get; set; }
    public int Amount { get; set; }
    public string? CostType { get; set; }
    public int CostId { get; set; }
    public int CostAmount { get; set; }
}

