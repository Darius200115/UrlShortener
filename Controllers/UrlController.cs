using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Data.Dto;
using UrlShortener.Data.Entities;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlShortenerService _urlShortenerService;
        private readonly UserManager<AppUser> _userManager;

        public UrlController(ILogger<UrlController> logger, IUrlShortenerService urlShortener, UserManager<AppUser> userManager)
        {
            _urlShortenerService = urlShortener;
            _logger = logger;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("RedirectToOriginal/{code}")]
        public ActionResult RedirectToOriginal(string code)
        {
            var longUrl = _urlShortenerService.RedirectToOriginalUrl(code);

            return Json(new { longUrl });

        }

        [HttpGet]
        public ActionResult<IEnumerable<UrlDetail>> GetAllUrls()
        {
            try
            {
                return Ok(_urlShortenerService.GetUrls());
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get urls: {ex}");
                return BadRequest("Failed to get urls");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> PostUrl([FromBody] UrlDto url)
        {
            try
            {
                //var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //url.User = currentUser;
                return Ok(_urlShortenerService.GenerateShortUrl(url.longUrl));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get short url: {ex}");
                return BadRequest("Failed to get short url!");
            }

        }

        [HttpGet]
        [Route("GetUrlById/{id}")]
        public IActionResult GetUrlById(int id)
        {
            try
            {
                var url = _urlShortenerService.GetUrlById(id);
                if (url != null)
                {
                    return Ok(_urlShortenerService.GetUrlById(id));
                }
                else { return NotFound(); }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get url {ex}");
                return BadRequest("Failed to get url");
            }
        }
      
    }
}
