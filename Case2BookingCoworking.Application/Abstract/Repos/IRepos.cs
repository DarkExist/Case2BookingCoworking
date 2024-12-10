using CSharpFunctionalExtensions;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Repos
{
    public interface IRepos<TEntity> where TEntity : Entity<Guid>
    {
        Task<ErrorOr<List<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ErrorOr<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> Attach(TEntity entity, CancellationToken cancellationToken);
    }
}
