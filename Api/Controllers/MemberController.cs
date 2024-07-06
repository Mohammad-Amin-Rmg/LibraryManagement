using Data.Contexts;
using Data.Contracts;
using Data.Repositories;
using Data.Repositories.Models;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ApplicationDbContext _context;
        public MemberController(IMemberRepository memberRepository, ApplicationDbContext context)
        {
            _memberRepository = memberRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<ApiResult<Member>> Create(MemberDto memberDto, CancellationToken cancellationToken)
        {
            var member = new Member
            {
                Name = memberDto.Name,
                PhoneNumber = memberDto.PhoneNumber,
            };

            await _memberRepository.AddMember(member, cancellationToken);
            return member;
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResult<List<string>>> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var book = await _memberRepository.GetByIdAsync(cancellationToken, id);
            book.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            var undeletedBooks = _memberRepository.TableNoTracking
                 .Where(x => x.IsDeleted == false)
                 .Select(x => x.Name).ToList();

            return undeletedBooks;
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResult> Update(int id, MemberDto memberDto, CancellationToken cancellationToken)
        {
            var updateMember = await _memberRepository.GetByIdAsync(cancellationToken, id);
            updateMember.Name = memberDto.Name;
            updateMember.PhoneNumber = memberDto.PhoneNumber;

            await _memberRepository.UpdateAsync(updateMember, cancellationToken);
            return Ok();
        }

        [HttpPost("borrow")]
        public async Task<ApiResult> Borrow([FromBody] BorrowDto borrowDto, CancellationToken cancellationToken)
        {
            //var query = _memberRepository.TableNoTracking;
            //query.Where(x=>x.Id == borrowDto.MemberId).Any(x=>x.Borrows.Any(x=>x.ReturnedDate == null ));
            //var x =  _context.Members.Select(x)

            var isMemberBorrowedBook = _context.Borrows.Where(x => x.MemberId == borrowDto.MemberId).Any(x => x.ReturnedDate == null);
            var isBookBorrowd = _context.Borrows.Where(x => x.BookId == borrowDto.BookId).Any(x => x.ReturnedDate == null);

            if (!(isMemberBorrowedBook && isBookBorrowd))
            {
                var newBorrow = new Borrow()
                {
                    BookId = borrowDto.BookId,
                    MemberId = borrowDto.MemberId,
                    BorrowDate = DateTime.Now
                };
            }
            else
            {
                return new ApiResult(false, ApiStatusCode.NotFound, "کتاب یا توسط فرد دیگری قرض گرغته شده است یا کاربر قبلا ان را قرض گرفته است");
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPut("returneborrow")]
        public async Task<ApiResult> ReturneBorrow(int borrowId, CancellationToken cancellationToken)
        {
            //var returnBorrow = _memberRepository.GetByIdAsync(cancellationToken, borrowDto.MemberId);
            //var borrow = _context.Borrows.Select(x => x.BorrowDate).FirstOrDefault();
            //var query = _context.Borrows.Where(x => x.Id == borrowDto.MemberId);

            var borrow = await _context.Borrows.FindAsync(borrowId);
            if (borrow == null)
            {
                return NotFound();
            }

            borrow.ReturnedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}

