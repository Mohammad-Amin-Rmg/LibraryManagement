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

        [HttpPost("{id:int}")]
        public async Task<ApiResult<Borrow>> Borrow(int id, [FromBody] Member member, CancellationToken cancellationToken)
        {
            var stateOfBook = _context.Books.Select(x => x.Borrows.Select(x => x.ReturnedDate));
            var stateOfMember = _context.Members.Select(x => x.Borrows.Select(x => x.ReturnedDate));
            if (stateOfMember is not null && stateOfMember is not null)
            {
                var newBorrow = new Borrow()
                {
                    BookId = id,
                    MemberId = member.Id,
                    BorrowDate = DateTime.Now
                };
                _context.Borrows.Add(newBorrow);
            }
            else
            {
                return NotFound("کتاب در امانت است");
            }

            return Ok();
        }

    }
}

