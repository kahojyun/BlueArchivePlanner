using BlueArchivePlanner.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;

namespace BlueArchivePlanner.Components;
public partial class ExportDialog : IDialogContentComponent
{
    [Inject]
    private UserPreferences UserPreferences { get; set; } = default!;

    private string Text { get; set; } = "";

    private void Export()
    {
        Text = UserPreferences.Export();
    }

    private void Import()
    {
        try
        {
            UserPreferences.Import(Text);
        }
        catch (JsonException)
        {
        }
    }
}