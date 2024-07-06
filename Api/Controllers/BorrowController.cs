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
    public class BorrowController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly IBorrowRepository _borrowRepository;
        public BorrowController(ApplicationDbContext dbContext, IBorrowRepository borrowRepository)
        {
            _dbContext = dbContext;
            _borrowRepository = borrowRepository;
        }

        [HttpGet("past")]
        public async Task<ApiResult<List<Borrow>>> Get()
        {
            var pastBorrowed = _dbContext.Borrows.Where(x => x.BorrowDate != null && x.ReturnedDate != null).ToList();
            return new ApiResult<List<Borrow>>(true, ApiStatusCode.Success, pastBorrowed);
        }


        [HttpGet("now")]
        public async Task<ApiResult<List<Borrow>>> GetNow()
        {
            var nowBorrowed = _dbContext.Borrows.Where(x => x.BorrowDate != null && x.ReturnedDate == null).ToList();
            return new ApiResult<List<Borrow>>(true, ApiStatusCode.Success, nowBorrowed);
        }

        [HttpGet("member")]
        public async Task<List<Borrow>> GetMemberBorrowedNow()
        {
            //var borrow = _dbContext.Members.Include(x => x.Borrows.Where(x => x.BorrowDate != null && x.ReturnedDate == null).ToList());
            var borrow = _dbContext.Members.SelectMany(x => x.Borrows.Where(x => x.BorrowDate != null && x.ReturnedDate == null))
                .Include(x=>x.Member)
                .Include(x=>x.Book)
                .ToList();
            return borrow;
        }

        [HttpGet("memberpast")]
        public async Task<ApiResult<List<Borrow>>> GetMemberBorrowedpast()
        {
            var nowBorrowed = _dbContext.Borrows.Where(x => x.BorrowDate != null && x.ReturnedDate == null).ToList();
            return new ApiResult<List<Borrow>>(true, ApiStatusCode.Success, nowBorrowed);
        }


    }
}
