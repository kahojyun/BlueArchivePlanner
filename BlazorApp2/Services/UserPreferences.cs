using BlazorApp2.Models;
using Microsoft.JSInterop;
using System.Text.Json;

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

    public string Export()
    {
        PreferenceObject data = new()
        {
            EquipmentCount = GetEquipmentCount(),
            StudentPreference = GetStudentPreferences()
        };
        return JsonSerializer.Serialize(data);
    }

    public void Import(string data)
    {
        PreferenceObject? obj = JsonSerializer.Deserialize<PreferenceObject>(data);
        if (obj is null)
        {
            return;
        }

        SetEquipmentCount(obj.EquipmentCount ?? []);
        SetStudentPreferences(obj.StudentPreference ?? []);
    }

    private class PreferenceObject
    {
        public Dictionary<int, int>? EquipmentCount { get; set; }
        public Dictionary<int, StudentPreference>? StudentPreference { get; set; }
    }
}
