using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FluentSliderRepro.Components;

public partial class MyFluentSlider<TValue> : FluentSlider<TValue>, IAsyncDisposable
    where TValue : System.Numerics.INumber<TValue>
{
    private const string JAVASCRIPT_FILE = "./MyFluentSlider.js";
    private ElementReference? container;

    /// <summary />
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary />
    private IJSObjectReference? Module { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Module ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE);
        }
        await Module!.InvokeVoidAsync("sliderUpdate", container);
    }
    public ValueTask DisposeAsync()
    {
        if (Module is not null)
        {
            return Module.DisposeAsync();
        }
        return ValueTask.CompletedTask;
    }
}
