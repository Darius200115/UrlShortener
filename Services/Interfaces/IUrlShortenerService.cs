using UrlShortener.Data.Entities;

namespace UrlShortener.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        string RedirectToOriginalUrl(string code);
        string GenerateShortUrl(string longUrl);
        string GenerateCode();
        IEnumerable<UrlDetail> GetUrls();
        UrlDetail GetUrlById(int id);
    }
}
