using System.Linq;

namespace Core.Infrastructure.Extensions
{
    public static class PagingExtension
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> data, int skip, int take)
        {
            return data
                .Skip(skip)
                .Take(take);
        }
    }
}
