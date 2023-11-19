using BlazorApp2.Models;
using Microsoft.JSInterop;

namespace BlazorApp2.Services;

public class UserPreferences(ILocalStorageService localStorageService)
{
    const string EquipmentCountKey = "countDict";
    const string StudentPreferenceKey = "studentPreference";
    public Dictionary<int, int>? GetEquipmentCount()
    {
        return localStorageService.GetItem<Dictionary<int, int>>(EquipmentCountKey);
    }

    public void SetEquipmentCount(Dictionary<int, int> data)
    {
        localStorageService.SetItem(EquipmentCountKey, data);
    }

    public Dictionary<int, StudentPreference>? GetStudentPreferences()
    {
        return localStorageService.GetItem<Dictionary<int, StudentPreference>>(StudentPreferenceKey);
    }

    public void SetStudentPreferences(Dictionary<int, StudentPreference> data)
    {
        localStorageService.SetItem(StudentPreferenceKey, data);
    }
}
