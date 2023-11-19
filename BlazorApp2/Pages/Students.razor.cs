using BlazorApp2.Models;
using BlazorApp2.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorApp2.Pages;
public partial class Students
{
    [Inject]
    private SchaleDb SchaleDb { get; set; } = default!;

    [Inject]
    private UserPreferences UserPreferences { get; set; } = default!;

    private bool IsLoading { get; set; } = true;

    private IQueryable<StudentViewModel> studentModels = default!;

    private StudentViewModel[] studentViewModels = [];

    private readonly GridSort<StudentViewModel> ownedSort = GridSort<StudentViewModel>.ByDescending(x => x.IsMember);

    protected override async Task OnInitializedAsync()
    {
        var data = await SchaleDb.GetStudentInfos();
        var preferences = UserPreferences.GetStudentPreferences();
        studentViewModels = data!.Values.Where(x => x.IsReleased![2])
            .Select(x =>
            {
                var pref = preferences?.GetValueOrDefault(x.Id);
                return new StudentViewModel(x.Name!, x.Id, pref?.EquipmentLevel ?? [1, 1, 1], pref?.IsMember ?? false);
            })
            .ToArray();
        studentModels = studentViewModels.AsQueryable();
        IsLoading = false;
    }

    private void SavePreferences()
    {
        var preferences = studentViewModels.ToDictionary(x => x.Id, x => new StudentPreference
        {
            EquipmentLevel = x.EquipmentLevel,
            IsMember = x.IsMember
        });
        UserPreferences.SetStudentPreferences(preferences);
    }

    private class StudentViewModel(string name, int id, int[] equipmentLevel, bool isMember)
    {
        public string Name => name;
        public int Id => id;
        public int[] EquipmentLevel => equipmentLevel;
        public bool IsMember { get; set; } = isMember;
    }
}