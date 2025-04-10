using BlueArchivePlanner.Services;
using Microsoft.AspNetCore.Components;

namespace BlueArchivePlanner.Pages;
public partial class Planning
{
    [Inject]
    private SchaleDb SchaleDb { get; set; } = default!;

    [Inject]
    private Calculator Calculator { get; set; } = default!;

    private bool isLoading = true;
    private PlanningViewModel[] planningViewModels = [];

    protected override async Task OnInitializedAsync()
    {
        Dictionary<int, int> plan = await Calculator.PlanningAsync();
        Dictionary<int, Models.Campaign> campaigns = await SchaleDb.GetCampaigns();
        planningViewModels = plan.Where(x => x.Value > 0).Select(x => new PlanningViewModel
        {
            StageName = GetStageName(x.Key),
            SweepCount = x.Value
        }).ToArray();
        isLoading = false;

        string GetStageName(int id)
        {
            Models.Campaign c = campaigns[id];
            return $"Stage {c.Area}-{c.Stage}";
        }
    }

    private class PlanningViewModel
    {
        public required string StageName { get; init; }
        public required int SweepCount { get; init; }
    }
}