using BlazorApp2.Models;

namespace BlazorApp2.Services;

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
        StudentInfo[] studentInfos = await schaleDb.GetStudentInfos();
        EquipmentInfo[] equipmentInfos = await schaleDb.GetEquipmentItems();
        IEnumerable<KeyValuePair<int, StudentPreference>> filtered = preferences.Where(x => x.Value.IsMember);

        foreach ((int studentId, StudentPreference p) in filtered)
        {
            StudentInfo studentInfo = studentInfos.First(x => x.Id == studentId);
            for (int i = 0; i < 3; i++)
            {
                int targetLevel = 5;
                int currentLevel = p.EquipmentLevel![i];
                string equipmentType = studentInfo.Equipment![i];
                IEnumerable<EquipmentInfo> equipment = equipmentInfos.Where(x => x.Category == equipmentType && x.Tier <= targetLevel && x.Tier > currentLevel && x.Recipe is not null);
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
}
