using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ApplicationDbContext _dbContext;
        public AuthorController(IAuthorRepository authorRepository, ApplicationDbContext dbContext)
        {
            _authorRepository = authorRepository;
           _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ApiResult<List<string>>> Get()
        {
            var author =  _authorRepository.TableNoTracking.Select(x=>x.Name).ToList();

            return author;
        }
        
    }
}
