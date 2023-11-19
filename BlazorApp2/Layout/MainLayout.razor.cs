using BlazorApp2.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorApp2.Layout;
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