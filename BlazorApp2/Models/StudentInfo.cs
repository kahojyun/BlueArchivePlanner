using System.Text.Json.Serialization;

namespace BlazorApp2.Models;

public class StudentInfo
{
    public int Id { get; set; }
    public bool[]? IsReleased { get; set; }
    public int DefaultOrder { get; set; }
    public string? PathName { get; set; }
    public string? DevName { get; set; }
    public string? Name { get; set; }
    public string? School { get; set; }
    public string? Club { get; set; }
    public int StarGrade { get; set; }
    public string? SquadType { get; set; }
    public string? TacticRole { get; set; }
    public Summon[]? Summons { get; set; }
    public string? Position { get; set; }
    public string? BulletType { get; set; }
    public string? ArmorType { get; set; }
    public int StreetBattleAdaptation { get; set; }
    public int OutdoorBattleAdaptation { get; set; }
    public int IndoorBattleAdaptation { get; set; }
    public string? WeaponType { get; set; }
    public string? WeaponImg { get; set; }
    public bool Cover { get; set; }
    public string[]? Equipment { get; set; }
    public string? CollectionBG { get; set; }
    public string? FamilyName { get; set; }
    public string? PersonalName { get; set; }
    public string? SchoolYear { get; set; }
    public string? CharacterAge { get; set; }
    public string? Birthday { get; set; }
    public string? CharacterSSRNew { get; set; }
    public string? ProfileIntroduction { get; set; }
    public string? Hobby { get; set; }
    public string? CharacterVoice { get; set; }
    public string? BirthDay { get; set; }
    public string? Illustrator { get; set; }
    public string? Designer { get; set; }
    public string? CharHeightMetric { get; set; }
    public string? CharHeightImperial { get; set; }
    public int StabilityPoint { get; set; }
    public int AttackPower1 { get; set; }
    public int AttackPower100 { get; set; }
    public int MaxHP1 { get; set; }
    public int MaxHP100 { get; set; }
    public int DefensePower1 { get; set; }
    public int DefensePower100 { get; set; }
    public int HealPower1 { get; set; }
    public int HealPower100 { get; set; }
    public int DodgePoint { get; set; }
    public int AccuracyPoint { get; set; }
    public int CriticalPoint { get; set; }
    public int CriticalDamageRate { get; set; }
    public int AmmoCount { get; set; }
    public int AmmoCost { get; set; }
    public int Range { get; set; }
    public int RegenCost { get; set; }
    public Skill[]? Skills { get; set; }
    public string[]? FavorStatType { get; set; }
    public int[][]? FavorStatValue { get; set; }
    public int?[]? FavorAlts { get; set; }
    public int[]? MemoryLobby { get; set; }
    public string? MemoryLobbyBGM { get; set; }
    public int[][][]? FurnitureInteraction { get; set; }
    public string[]? FavorItemTags { get; set; }
    public string[]? FavorItemUniqueTags { get; set; }
    public int IsLimited { get; set; }
    public Weapon? Weapon { get; set; }
    public Gear? Gear { get; set; }
    public int[][]? SkillExMaterial { get; set; }
    public int[][]? SkillExMaterialAmount { get; set; }
    public int[][]? SkillMaterial { get; set; }
    public int[][]? SkillMaterialAmount { get; set; }
}

public class Weapon
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public string? AdaptationType { get; set; }
    public int AdaptationValue { get; set; }
    public int AttackPower1 { get; set; }
    public int AttackPower100 { get; set; }
    public int MaxHP1 { get; set; }
    public int MaxHP100 { get; set; }
    public int HealPower1 { get; set; }
    public int HealPower100 { get; set; }
    public string? StatLevelUpType { get; set; }
}

public class Gear
{
    public bool[]? Released { get; set; }
    public string[]? StatType { get; set; }
    public int[][]? StatValue { get; set; }
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public int[][]? TierUpMaterial { get; set; }
    public int[][]? TierUpMaterialAmount { get; set; }
}

public class Summon
{
    public int Id { get; set; }
    public string? SourceSkill { get; set; }
    public string[]? InheritCasterStat { get; set; }
    public int[][]? InheritCasterAmount { get; set; }
    public int ObstacleMaxHP1 { get; set; }
    public int ObstacleMaxHP100 { get; set; }
}

public class Skill
{
    public string? SkillType { get; set; }
    public Effect[]? Effects { get; set; }
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public string[][]? Parameters { get; set; }
    public int[]? Cost { get; set; }
    public int Duration { get; set; }
    public int Range { get; set; }
    public RadiusInfo[]? Radius { get; set; }
    public string? Icon { get; set; }
    public string[]? EffectCombine { get; set; }
    public EffectCombineLabel? EffectCombineLabel { get; set; }
    public InheritScale? InheritScale { get; set; }
    public bool HideCalculation { get; set; }
}

public class EffectCombineLabel
{
    public string[]? Icon { get; set; }
    public string[]? StackLabelTranslated { get; set; }
    public bool DisableFirst { get; set; }
    public string[]? StackLabel { get; set; }
}

public class InheritScale
{
    public string? Skill { get; set; }
    public int EffectId { get; set; }
    public int Parameter { get; set; }
}

public class Effect
{
    public string? Type { get; set; }
    public int[]? Hits { get; set; }
    public int[]? Scale { get; set; }
    public Frames? Frames { get; set; }
    public string? CriticalCheck { get; set; }
    public string? Stat { get; set; }
    public int[][]? Value { get; set; }
    public int Channel { get; set; }
    public string? Duration { get; set; }
    public string? Period { get; set; }
    public int HitsParameter { get; set; }
    public string? Chance { get; set; }
    public string? Icon { get; set; }
    public string? SubstituteCondition { get; set; }
    public int[]? SubstituteScale { get; set; }
    public int[]? HitFrames { get; set; }
    public int StackSame { get; set; }
    public int[]? IgnoreDef { get; set; }
    public Restriction[]? Restrictions { get; set; }
    public int ZoneHitInterval { get; set; }
    public int ZoneDuration { get; set; }
    public int Critical { get; set; }
    public bool HideFormChangeIcon { get; set; }
    public string? SourceStat { get; set; }
    public ExtraDamageSource? ExtraDamageSource { get; set; }
}

public class Frames
{
    public int AttackEnterDuration { get; set; }
    public int AttackStartDuration { get; set; }
    public int AttackEndDuration { get; set; }
    public int AttackBurstRoundOverDelay { get; set; }
    public int AttackIngDuration { get; set; }
    public int AttackReloadDuration { get; set; }
    public int AttackReadyStartDuration { get; set; }
    public int AttackReadyEndDuration { get; set; }
}

public class ExtraDamageSource
{
    public string? Side { get; set; }
    public string? Stat { get; set; }
    public int[]? Multiplier { get; set; }
    public string? SliderTranslation { get; set; }
    public float[]? SliderStep { get; set; }
    public int[]? SliderLabel { get; set; }
    public string? SliderLabelSuffix { get; set; }
    public bool SimulatePerHit { get; set; }
}

public class Restriction
{
    public string? Property { get; set; }
    public string? Operand { get; set; }
    public object? Value { get; set; }
}

public class RadiusInfo
{
    public string? Type { get; set; }
    public int Radius { get; set; }
    public int Degree { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int ExcludeRadius { get; set; }
}
