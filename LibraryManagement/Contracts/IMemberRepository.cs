using LibraryManagement.Models;

namespace Data.Contracts
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task AddMember(Member member, CancellationToken cancellationToken);
    }
}