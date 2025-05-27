namespace ArtistPortfolio.Web.Services;

public interface IToastService
{
    Task ShowSuccess(string message, string title = "Başarılı");
    Task ShowError(string message, string title = "Hata");
    Task ShowWarning(string message, string title = "Uyarı");
    Task ShowInfo(string message, string title = "Bilgi");
}