using BlueArchivePlanner.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;

namespace BlueArchivePlanner.Components;
public partial class ExportDialog : IDialogContentComponent
{
    [Inject]
    private UserPreferences UserPreferences { get; set; } = default!;

    private string ExportText { get; set; } = "";
    private string ImportText { get; set; } = "";

    private void Export()
    {
        ExportText = UserPreferences.Export();
    }

    private void Import()
    {
        try
        {
            UserPreferences.Import(ImportText);
        }
        catch (JsonException)
        {
        }
    }
}