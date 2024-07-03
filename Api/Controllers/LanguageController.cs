using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ApplicationDbContext _context;

        public LanguageController(ILanguageRepository languageRepository, ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<Language>>> Get()
        {
            var language = _languageRepository.TableNoTracking.ToList();
            return language;
        }

    }
}
