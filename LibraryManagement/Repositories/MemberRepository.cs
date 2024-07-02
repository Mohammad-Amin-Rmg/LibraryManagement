using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
