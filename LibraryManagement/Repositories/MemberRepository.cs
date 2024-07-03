using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MemberRepository : Repository<Member>,IMemberRepository
    {
        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task AddMember(Member member, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(x => x.Name == member.Name);
            if (exists)
                throw new Exception("نام کاربری تکراری است");

            await base.Add(member,cancellationToken);
        }
    }
}
