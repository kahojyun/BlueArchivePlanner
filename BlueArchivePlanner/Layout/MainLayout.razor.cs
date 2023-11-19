using BlueArchivePlanner.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlueArchivePlanner.Layout;
public partial class MainLayout
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task OpenExportAsync()
    {
        await DialogService.ShowDialogAsync<ExportDialog>(new DialogParameters()
        {
            Title = "Export",
        });
    }
}