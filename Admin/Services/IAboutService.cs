using ArtistPortfolio.Shared.Models;

namespace ArtistPortfolio.Admin.Services;

public interface IAboutService
{
    /// <summary>
    /// Tüm hakkımda kayıtlarını getirir
    /// </summary>
    /// <returns>Hakkımda kayıtları listesi</returns>
    Task<List<About>> GetAboutsAsync();
    
    /// <summary>
    /// Belirtilen ID'ye sahip hakkımda kaydını getirir
    /// </summary>
    /// <param name="id">Hakkımda kaydının ID'si</param>
    /// <returns>Hakkımda kaydı veya null</returns>
    Task<About?> GetAboutAsync(int id);
    
    /// <summary>
    /// Yeni hakkımda kaydı oluşturur
    /// </summary>
    /// <param name="about">Oluşturulacak hakkımda kaydı</param>
    /// <returns>Oluşturulan hakkımda kaydı</returns>
    Task<About> CreateAboutAsync(About about);
    
    /// <summary>
    /// Mevcut hakkımda kaydını günceller
    /// </summary>
    /// <param name="about">Güncellenecek hakkımda kaydı</param>
    /// <returns>Güncellenen hakkımda kaydı</returns>
    Task<About> UpdateAboutAsync(About about);
    
    /// <summary>
    /// Belirtilen ID'ye sahip hakkımda kaydını siler
    /// </summary>
    /// <param name="id">Silinecek hakkımda kaydının ID'si</param>
    /// <returns>Silme işlemi başarılı ise true, değilse false</returns>
    Task<bool> DeleteAboutAsync(int id);
    
    /// <summary>
    /// Sadece aktif hakkımda kayıtlarını getirir
    /// </summary>
    /// <returns>Aktif hakkımda kayıtları listesi</returns>
    Task<List<About>> GetActiveAboutsAsync();
    
    /// <summary>
    /// Hakkımda kaydının aktif/pasif durumunu değiştirir
    /// </summary>
    /// <param name="id">Durumu değiştirilecek kaydın ID'si</param>
    /// <param name="isActive">Yeni aktif durumu</param>
    /// <returns>İşlem başarılı ise true, değilse false</returns>
    Task<bool> ToggleActiveStatusAsync(int id, bool isActive);
    
    /// <summary>
    /// Hakkımda kayıtlarını sayfalı olarak getirir
    /// </summary>
    /// <param name="page">Sayfa numarası</param>
    /// <param name="pageSize">Sayfa boyutu</param>
    /// <returns>Sayfalı hakkımda kayıtları</returns>
    Task<(List<About> Items, int TotalCount)> GetPagedAboutsAsync(int page, int pageSize);
    
    /// <summary>
    /// Hakkımda kayıtlarını arama terimine göre filtreler
    /// </summary>
    /// <param name="searchTerm">Arama terimi</param>
    /// <returns>Filtrelenmiş hakkımda kayıtları</returns>
    Task<List<About>> SearchAboutsAsync(string searchTerm);
    
    /// <summary>
    /// Belirtilen tarih aralığındaki hakkımda kayıtlarını getirir
    /// </summary>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <returns>Tarih aralığındaki hakkımda kayıtları</returns>
    Task<List<About>> GetAboutsByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Hakkımda kayıtlarının toplam sayısını getirir
    /// </summary>
    /// <returns>Toplam kayıt sayısı</returns>
    Task<int> GetAboutsCountAsync();
    
    /// <summary>
    /// Aktif hakkımda kayıtlarının sayısını getirir
    /// </summary>
    /// <returns>Aktif kayıt sayısı</returns>
    Task<int> GetActiveAboutsCountAsync();
}