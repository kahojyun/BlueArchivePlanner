using Microsoft.AspNetCore.Components;

namespace BlueArchivePlanner.Components;
public partial class EquipmentCard
{
    [Parameter, EditorRequired]
    public string Icon { get; set; } = default!;

    [Parameter, EditorRequired]
    public int Count { get; set; }

    [Parameter]
    public EventCallback<int> CountChanged { get; set; }

    [Parameter, EditorRequired]
    public int Demand { get; set; }

    private string DemandColor => Count >= Demand ? "green" : "red";

    private int CurrentCount
    {
        get => Count;
        set => CountChanged.InvokeAsync(value);
    }
}