namespace BlazorApp2.Models;

public class ConfigInfo
{
    public Link[]? links { get; set; }
    public int build { get; set; }
    public Region[]? Regions { get; set; }
    public Changelog[]? Changelog { get; set; }
    public TypeEffectiveness? TypeEffectiveness { get; set; }
    public GachaGroup[]? GachaGroups { get; set; }
}

public class TypeEffectiveness
{
    public NormalCls? Normal { get; set; }
    public Explosion? Explosion { get; set; }
    public Pierce? Pierce { get; set; }
    public Mystic? Mystic { get; set; }
    public Sonic? Sonic { get; set; }
}

public class NormalCls
{
    public int LightArmor { get; set; }
    public int HeavyArmor { get; set; }
    public int Unarmed { get; set; }
    public int Structure { get; set; }
    public int ElasticArmor { get; set; }
    public int Normal { get; set; }
}

public class Explosion
{
    public int LightArmor { get; set; }
    public int HeavyArmor { get; set; }
    public int Unarmed { get; set; }
    public int Structure { get; set; }
    public int ElasticArmor { get; set; }
    public int Normal { get; set; }
}

public class Pierce
{
    public int LightArmor { get; set; }
    public int HeavyArmor { get; set; }
    public int Unarmed { get; set; }
    public int Structure { get; set; }
    public int ElasticArmor { get; set; }
    public int Normal { get; set; }
}

public class Mystic
{
    public int LightArmor { get; set; }
    public int HeavyArmor { get; set; }
    public int Unarmed { get; set; }
    public int Structure { get; set; }
    public int ElasticArmor { get; set; }
    public int Normal { get; set; }
}

public class Sonic
{
    public int LightArmor { get; set; }
    public int HeavyArmor { get; set; }
    public int Unarmed { get; set; }
    public int Structure { get; set; }
    public int ElasticArmor { get; set; }
    public int Normal { get; set; }
}

public class Link
{
    public string? section { get; set; }
    public Content[]? content { get; set; }
}

public class Content
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? url { get; set; }
    public string? author { get; set; }
}

public class Region
{
    public string? Name { get; set; }
    public int StudentMaxLevel { get; set; }
    public int WeaponMaxLevel { get; set; }
    public int BondMaxLevel { get; set; }
    public int[]? EquipmentMaxLevel { get; set; }
    public bool GearUnlock { get; set; }
    public int[]? GearBondReq { get; set; }
    public int CampaignMax { get; set; }
    public bool CampaignExtra { get; set; }
    public int[]? Events { get; set; }
    public int[]? Event701Max { get; set; }
    public int ChaserMax { get; set; }
    public int BloodMax { get; set; }
    public int FindGiftMax { get; set; }
    public int SchoolDungeonMax { get; set; }
    public int FurnitureSetMax { get; set; }
    public int FurnitureTemplateMax { get; set; }
    public CurrentGacha[]? CurrentGacha { get; set; }
    public CurrentEvent[]? CurrentEvents { get; set; }
    public CurrentRaid[]? CurrentRaid { get; set; }
}

public class CurrentGacha
{
    public int[]? characters { get; set; }
    public int start { get; set; }
    public int end { get; set; }
}

public class CurrentEvent
{
    public int _event { get; set; }
    public int start { get; set; }
    public int end { get; set; }
}

public class CurrentRaid
{
    public string? type { get; set; }
    public int raid { get; set; }
    public string? terrain { get; set; }
    public int season { get; set; }
    public int start { get; set; }
    public int end { get; set; }
}

public class Changelog
{
    public string? date { get; set; }
    public string[]? contents { get; set; }
}

public class GachaGroup
{
    public int Id { get; set; }
    public int[][]? ItemList { get; set; }
}
