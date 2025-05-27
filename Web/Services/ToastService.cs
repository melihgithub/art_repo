using Microsoft.JSInterop;

namespace ArtistPortfolio.Web.Services;

public class ToastService : IToastService
{
    private readonly IJSRuntime _jsRuntime;

    public ToastService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ShowSuccess(string message, string title = "Başarılı")
    {
        await _jsRuntime.InvokeVoidAsync("showToast", title, message, "success");
    }

    public async Task ShowError(string message, string title = "Hata")
    {
        await _jsRuntime.InvokeVoidAsync("showToast", title, message, "error");
    }

    public async Task ShowWarning(string message, string title = "Uyarı")
    {
        await _jsRuntime.InvokeVoidAsync("showToast", title, message, "warning");
    }

    public async Task ShowInfo(string message, string title = "Bilgi")
    {
        await _jsRuntime.InvokeVoidAsync("showToast", title, message, "info");
    }
}