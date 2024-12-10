using Case2BookingCoworking.Infrastructure.Data.Context;
using CSharpFunctionalExtensions;
using ErrorOr;
using Case2BookingCoworking.Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;
namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    public abstract class BaseRepository<T> where T : Entity<Guid>
    {
        protected readonly BookingContext _context;

        public BaseRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Success>> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return ErrorOr.Result.Success;
            }
            catch (OperationCanceledException)
            {
                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Updating {typeof(T)} {entity.Id} was failed.");
            }
        }
        public async Task<ErrorOr<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id, cancellationToken);

                if (entity is null)
                {
                    return Error.NotFound(description: $"Entity with this Id:{id} not found");
                }

                return entity;
            }
            catch (OperationCanceledException)
            {
                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Getting {typeof(T)} with id: {id} was failed");
            }
        }

        public async Task<ErrorOr<Success>> DeleteAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _context.Set<T>().Find(Id);
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return ErrorOr.Result.Success;
            }
            catch (OperationCanceledException)
            {
                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Deleting entity: {typeof(T)} was failed");
            }
        }
        public async Task<ErrorOr<Success>> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return ErrorOr.Result.Success;
            }
            catch (OperationCanceledException)
            {
                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Adding entity: {entity} was failed");
            }
        }

        public async Task<ErrorOr<List<T>>> GetManyByIdAsync(List<Guid> listOfId, CancellationToken cancellationToken)
        {
            try
            {
                var matches = _context.Set<T>().Where(e => listOfId.Contains(e.Id)).ToList();

                return matches;
            }
            catch (OperationCanceledException)
            {
                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Gettings entities by listOfId query was failed");
            }
        }

        public async Task<ErrorOr<List<T>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);

                if (entities is null)
                {
                    return Error.NotFound(description: $"entities {typeof(T)} was null");
                }

                return entities;
            }
            catch (OperationCanceledException)
            {
                return
                   new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" Getting all entites was failed");
            }
        }

        public async Task<ErrorOr<Success>> Attach(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _context.Attach(entity);
                return ErrorOr.Result.Success;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $"Attaching entity was failed");
            }
        }
    }
}
