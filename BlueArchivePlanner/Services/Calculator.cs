using BlueArchivePlanner.Models;

namespace BlueArchivePlanner.Services;

public class Calculator(UserPreferences userPreferences, SchaleDb schaleDb)
{
    public async Task<Dictionary<int, int>> CalculateDemandAsync()
    {
        Dictionary<int, StudentPreference>? preferences = userPreferences.GetStudentPreferences();
        if (preferences is null)
        {
            return [];
        }

        Dictionary<int, int> demand = [];
        Dictionary<int, StudentInfo> studentInfos = await schaleDb.GetStudentInfos();
        Dictionary<int, EquipmentInfo> equipmentInfos = await schaleDb.GetEquipmentInfos();
        IEnumerable<KeyValuePair<int, StudentPreference>> filtered = preferences.Where(x => x.Value.IsMember);

        foreach ((int studentId, StudentPreference p) in filtered)
        {
            StudentInfo studentInfo = studentInfos[studentId];
            for (int i = 0; i < 3; i++)
            {
                int targetLevel = 5;
                int currentLevel = p.EquipmentLevel![i];
                string equipmentType = studentInfo.Equipment![i];
                IEnumerable<EquipmentInfo> equipment = equipmentInfos.Values.Where(x => x.Category == equipmentType
                                                                                        && x.Tier <= targetLevel
                                                                                        && x.Tier > currentLevel
                                                                                        && x.Recipe is not null);
                foreach (EquipmentInfo? e in equipment)
                {
                    foreach (int[] r in e.Recipe!)
                    {
                        int recipeId = r[0];
                        int recipeNumber = r[1];
                        if (demand.ContainsKey(recipeId))
                        {
                            demand[recipeId] += recipeNumber;
                        }
                        else
                        {
                            demand[recipeId] = recipeNumber;
                        }
                    }
                }
            }
        }

        return demand;
    }

    public async Task<Dictionary<int, int>> CalculateNeededAsync()
    {
        var demand = await CalculateDemandAsync();
        var current = userPreferences.GetEquipmentCount();
        return demand.Select(x => (x.Key, Value: x.Value - (current?.GetValueOrDefault(x.Key) ?? 0))).Where(x => x.Value > 0).ToDictionary();
    }

    public async Task<Dictionary<int, int>> PlanningAsync()
    {
        Dictionary<int, Campaign> campaigns = await schaleDb.GetCampaigns();
        Dictionary<int, GachaGroup> gachaGroups = await schaleDb.GetGachaGroups();
        Dictionary<int, int> demand = await CalculateNeededAsync();
        Campaign[] stages = campaigns.Values.Where(x => x.Area <= 14 && x.Difficulty == 0 && x.EntryCost is [[5, ..]] && CheckDemand(x)).ToArray();
        int nStages = stages.Length;
        int nDemand = demand.Count;
        Dictionary<int, int> stageIndex = stages.Select((x, i) => (x.Id, i)).ToDictionary(x => x.Id, x => x.i);
        Dictionary<int, int> demandIndex = demand.Select((x, i) => (x.Key, i)).ToDictionary(x => x.Key, x => x.i);
        double[] objective = BuildObjective();
        double[,] constraints = BuildConstraints();
        double[] rightHandSides = BuildRightHandSides();
        Tuple<double, double[]> result = Optimization.SolveLinearProgram(objective, constraints, rightHandSides);
        double[] solution = result.Item2;
        Dictionary<int, int> plan = [];
        foreach ((int id, int i) in stageIndex)
        {
            plan[id] = (int)Math.Ceiling(solution[i]);
        }
        return plan;


        bool CheckDemand(Campaign x)
        {
            double[][] rewards = (x.RewardsCn is not null) ? x.RewardsCn.Default! : x.Rewards!.Default!;
            foreach (double[] r in rewards)
            {
                int id = (int)r[0];
                if (id >= 2000000 && id < 3000000)
                {
                    if (demand.ContainsKey(id - 2000000))
                    {
                        return true;
                    }
                }
                else if (id >= 4000000 && id < 5000000)
                {
                    GachaGroup gacha = gachaGroups[id - 4000000];
                    foreach (int[] item in gacha.ItemList!)
                    {
                        if (demand.ContainsKey(item[0] - 2000000))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        double[] BuildObjective()
        {
            double[] objective = new double[nStages];
            foreach ((int id, int i) in stageIndex)
            {
                objective[i] = -campaigns[id].EntryCost![0][1];
            }
            return objective;
        }

        double[,] BuildConstraints()
        {
            double[,] constraints = new double[nDemand, nStages];
            foreach (Campaign? stage in stages)
            {
                double[][] rewards = (stage.RewardsCn is not null) ? stage.RewardsCn.Default! : stage.Rewards!.Default!;
                foreach (double[] r in rewards)
                {
                    int id = (int)r[0];
                    if (id >= 2000000 && id < 3000000)
                    {
                        TrySetConstraint(id - 2000000, stage.Id, r[1]);
                    }
                    else if (id >= 4000000 && id < 5000000)
                    {
                        GachaGroup gacha = gachaGroups[id - 4000000];
                        foreach ((int gachaItemId, double prob) in CalculateGachaProbability(gacha))
                        {
                            TrySetConstraint(gachaItemId - 2000000, stage.Id, prob * r[1]);
                        }
                    }
                }
            }

            return constraints;

            void TrySetConstraint(int demandId, int stageId, double value)
            {
                if (demandIndex.TryGetValue(demandId, out int i) && stageIndex.TryGetValue(stageId, out int j))
                {
                    constraints[i, j] = value;
                }
            }

            static IEnumerable<(int Id, double Probability)> CalculateGachaProbability(GachaGroup x)
            {
                int total = x.ItemList!.Sum(x => x[1]);
                return x.ItemList!.Select(x => (x[0], (double)x[1] / total));
            }
        }

        double[] BuildRightHandSides()
        {
            double[] rightHandSides = new double[nDemand];
            foreach ((int id, int i) in demandIndex)
            {
                rightHandSides[i] = demand[id];
            }
            return rightHandSides;
        }
    }
}
