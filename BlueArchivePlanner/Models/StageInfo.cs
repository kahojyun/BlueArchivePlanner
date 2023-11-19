namespace BlueArchivePlanner.Models;

public class StageInfo
{
    public Campaign[]? Campaign { get; set; }
    public Event[]? Event { get; set; }
    public WeekDungeon[]? WeekDungeon { get; set; }
    public SchoolDungeon[]? SchoolDungeon { get; set; }
    public Conquest[]? Conquest { get; set; }
    public ConquestMap[]? ConquestMap { get; set; }
}

public class Campaign
{
    public int Id { get; set; }
    public int Difficulty { get; set; }
    public int Area { get; set; }
    public object? Stage { get; set; }
    public string? NameEn { get; set; }
    public string? NameJp { get; set; }
    public string? NameKr { get; set; }
    public string? NameTw { get; set; }
    public string? NameTh { get; set; }
    public string? NameCn { get; set; }
    public string? NameZh { get; set; }
    public string? NameVi { get; set; }
    public int[][]? EntryCost { get; set; }
    public int[]? StarCondition { get; set; }
    public object[][]? ChallengeCondition { get; set; }
    public string? Terrain { get; set; }
    public int Level { get; set; }
    public Rewards? Rewards { get; set; }
    public Formation[]? Formations { get; set; }
    public Hexamap[]? HexaMap { get; set; }
    public RewardsGlobal? RewardsGlobal { get; set; }
    public RewardsCn? RewardsCn { get; set; }
}

public class Rewards
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsGlobal
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsCn
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class Formation
{
    public int Id { get; set; }
    public string? MapIcon { get; set; }
    public string? MoveType { get; set; }
    public string? UnitGrade { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
}

public class Hexamap
{
    public string? Type { get; set; }
    public int[]? Pos { get; set; }
    public int Entity { get; set; }
    public int Target { get; set; }
    public int Trigger { get; set; }
}

public class Event
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string[]? Versions { get; set; }
    public int Difficulty { get; set; }
    public object? Stage { get; set; }
    public string? NameEn { get; set; }
    public string? NameJp { get; set; }
    public string? NameKr { get; set; }
    public string? NameTw { get; set; }
    public string? NameTh { get; set; }
    public string? NameCn { get; set; }
    public string? NameZh { get; set; }
    public string? NameVi { get; set; }
    public int[][]? EntryCost { get; set; }
    public int[]? StarCondition { get; set; }
    public object[][]? ChallengeCondition { get; set; }
    public int Level { get; set; }
    public string? Terrain { get; set; }
    public Formation1[]? Formations { get; set; }
    public HexaMap1[]? HexaMap { get; set; }
    public Rewards1? Rewards { get; set; }
    public RewardsGlobal1? RewardsGlobal { get; set; }
    public RewardsCn1? RewardsCn { get; set; }
    public int[][]? EntryCostRerun { get; set; }
    public object[][]? ChallengeConditionRerun { get; set; }
    public RewardsRerun? RewardsRerun { get; set; }
    public RewardsCnRerun? RewardsCnRerun { get; set; }
    public int[]? StarConditionRerun { get; set; }
    public FormationsRerun[]? FormationsRerun { get; set; }
    public object? HexaMapRerun { get; set; }
}

public class Rewards1
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsGlobal1
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsCn1
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsRerun
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class RewardsCnRerun
{
    public int[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class Formation1
{
    public int Id { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
    public string? MapIcon { get; set; }
    public string? MoveType { get; set; }
    public string? UnitGrade { get; set; }
}

public class HexaMap1
{
    public string? Type { get; set; }
    public int[]? Pos { get; set; }
    public int Entity { get; set; }
    public int Target { get; set; }
    public int Trigger { get; set; }
}

public class FormationsRerun
{
    public int Id { get; set; }
    public string? MapIcon { get; set; }
    public string? MoveType { get; set; }
    public string? UnitGrade { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
}

public class WeekDungeon
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public int Stage { get; set; }
    public int[][]? EntryCost { get; set; }
    public int[]? StarCondition { get; set; }
    public string? Terrain { get; set; }
    public int Level { get; set; }
    public Rewards2? Rewards { get; set; }
    public RewardsGlobal2? RewardsGlobal { get; set; }
    public RewardsCn2? RewardsCn { get; set; }
    public Formation2[]? Formations { get; set; }
}

public class Rewards2
{
    public double[][]? Default { get; set; }
}

public class RewardsGlobal2
{
    public double[][]? Default { get; set; }
}

public class RewardsCn2
{
    public double[][]? Default { get; set; }
}

public class Formation2
{
    public int Id { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
}

public class SchoolDungeon
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public int Stage { get; set; }
    public int[][]? EntryCost { get; set; }
    public int[]? StarCondition { get; set; }
    public string? Terrain { get; set; }
    public int Level { get; set; }
    public Rewards3? Rewards { get; set; }
    public Formation3[]? Formations { get; set; }
}

public class Rewards3
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
}

public class Formation3
{
    public int Id { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
}

public class Conquest
{
    public int Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameJp { get; set; }
    public string? NameKr { get; set; }
    public string? NameTw { get; set; }
    public string? NameTh { get; set; }
    public string? NameCn { get; set; }
    public string? NameZh { get; set; }
    public string? NameVi { get; set; }
    public int EventId { get; set; }
    public Formation4[]? Formations { get; set; }
    public string? Difficulty { get; set; }
    public int Level { get; set; }
    public string? EnemyType { get; set; }
    public string? Terrain { get; set; }
    public int Step { get; set; }
    public string? Team { get; set; }
    public bool SubStage { get; set; }
    public int[]? StarCondition { get; set; }
    public int[][]? EntryCost { get; set; }
    public string[][]? SchoolBuff { get; set; }
    public int[][]? UpgradeCost { get; set; }
    public Rewards4? Rewards { get; set; }
}

public class Rewards4
{
    public double[][]? Default { get; set; }
    public int[][]? FirstClear { get; set; }
    public int[][]? ThreeStar { get; set; }
    public int[][][]? Calculate { get; set; }
    public int[][]? Upgrade2 { get; set; }
    public int[][]? Upgrade3 { get; set; }
}

public class Formation4
{
    public int Id { get; set; }
    public string? MapIcon { get; set; }
    public int[]? Level { get; set; }
    public int[]? Grade { get; set; }
    public int[]? EnemyList { get; set; }
}

public class ConquestMap
{
    public int EventId { get; set; }
    public Map[]? Maps { get; set; }
}

public class Map
{
    public string? Name { get; set; }
    public int Step { get; set; }
    public string? Difficulty { get; set; }
    public Tile[]? Tiles { get; set; }
}

public class Tile
{
    public long Id { get; set; }
    public string? Type { get; set; }
    public int[]? Pos { get; set; }
    public int StageId { get; set; }
}
