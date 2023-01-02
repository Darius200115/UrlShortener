using UrlShortener.Data;
using UrlShortener.Data.Entities;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Services.Implementation
{
    public class UrlShortenerService : IUrlShortenerService
    {
        const string safeShortCode = "65464345345234546566786785654345354364547567587686765343546546";
        readonly AppDbContext _context;
        public UrlShortenerService(AppDbContext context)
        {
            _context = context;
        }

        public string GenerateShortUrl(string longUrl)
        {
            var code = GenerateCode();
            while (_context.UrlDetails.ToList().Where(x => x.Code == code).Any())
            {
                code = GenerateCode();
            }

            var url = new UrlDetail()
            {
                CreatedDate = DateTime.Now,
                LongUrl = longUrl,
                Code = code,

            };

            _context.UrlDetails.Add(url);
            _context.SaveChanges();

            return code;

        }

        public string GenerateCode()
        {
            return safeShortCode.Substring(new Random().Next(0, safeShortCode.Length), new Random().Next(3, 5));
        }

        public string RedirectToOriginalUrl(string code)
        {

            return _context.UrlDetails.Where(x => x.Code == code).FirstOrDefault().LongUrl;
        }

        public IEnumerable<UrlDetail> GetUrls()
        {
            return _context.UrlDetails.OrderBy(p => p.Id).ToList();
        }

        public UrlDetail GetUrlById(int id)
        {
            return _context.UrlDetails.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
