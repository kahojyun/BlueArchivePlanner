using BlueArchivePlanner.Models;
using BlueArchivePlanner.Services;
using Microsoft.AspNetCore.Components;

namespace BlueArchivePlanner.Pages;
public partial class Equipment
{
    [Inject]
    private SchaleDb SchaleDb { get; set; } = default!;

    [Inject]
    private UserPreferences UserPreferences { get; set; } = default!;

    [Inject]
    private Calculator Calculator { get; set; } = default!;

    private EquipmentViewModel[] equipmentModels = [];

    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        Dictionary<int, EquipmentInfo> data = await SchaleDb.GetEquipmentInfos();
        IEnumerable<EquipmentInfo> filtered = data!.Values.Where(x => x.IsReleased![2] && x.Icon!.EndsWith("piece"));
        Dictionary<int, int>? stored = UserPreferences.GetEquipmentCount();
        Dictionary<int, int> demand = await Calculator.CalculateDemandAsync();
        equipmentModels = filtered.Select(x => new EquipmentViewModel
        {
            Icon = x.Icon!,
            Count = stored?.GetValueOrDefault(x.Id) ?? 0,
            Id = x.Id,
            Demand = demand.GetValueOrDefault(x.Id)
        }).ToArray();
        isLoading = false;
    }



    private void SaveCount()
    {
        Dictionary<int, int> countDict = equipmentModels.ToDictionary(x => x.Id, x => x.Count);
        UserPreferences.SetEquipmentCount(countDict);
    }

    private class EquipmentViewModel
    {
        public required string Icon { get; init; }
        public required int Count { get; set; }
        public required int Id { get; init; }
        public required int Demand { get; init; }
    }
}