using NHibernate;
using NHibernate.Criterion.Lambda;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public static class ServiceExtension
    {
        public static IQueryable<T> Where<T, PropT>(this IQueryable<T> self, string propertyName, Expression<Func<PropT, bool>> predicate)
        {
            var paramExpr = Expression.Parameter(typeof(T));
            var propExpr = Expression.Property(paramExpr, propertyName);
            return self.Where(Expression.Lambda<Func<T, bool>>(Expression.Invoke(predicate, propExpr), paramExpr));
        }

        public static async Task<List<T1>> ToListByListKeyAsync<T1>(this IQueryable<T1> query, List<string> list, string propertyName, int batchSize = 1000)
        {
            int i = 0;
            List<T1> result = new List<T1>();
            while (list.Count >= i * batchSize)
            {
                var dt = list.Skip(i * batchSize).Take(batchSize).ToList();
                var batchResult = await query
                    .Where<T1, string>(propertyName, c => dt.Contains(c))
                    .ToListAsync();
                result.AddRange(batchResult);
                i++;
            }
            return result;
        }

        public static async Task<List<T1>> DeleteAsync<T1>(this IQueryable<T1> query, List<string> list, string propertyName, int batchSize = 1000)
        {
            int i = 0;
            List<T1> result = new List<T1>();
            while (list.Count >= i * batchSize)
            {
                var dt = list.Skip(i * batchSize).Take(batchSize).ToList();
                var batchResult = await query
                    .Where<T1, string>(propertyName, c => dt.Contains(c))
                    .DeleteAsync();
                i++;
            }
            return result;
        }

        public static List<UpdateBuilder<T1>> GetUpdate<T1>(this IQueryable<T1> query, List<string> list, string propertyName, int batchSize = 1000)
        {
            int i = 0;
            List<UpdateBuilder<T1>> updateBuilders = new List<UpdateBuilder<T1>>();
            List<T1> result = new List<T1>();
            while (list.Count >= i * batchSize)
            {
                var dt = list.Skip(i * batchSize).Take(batchSize).ToList();
                i++;
                updateBuilders.Add(query
                    .Where<T1, string>(propertyName, c => dt.Contains(c))
                    .UpdateBuilder());
            }
            return updateBuilders;
        }

        public static async Task<List<T1>> ToListByListKeyAsync<T1>(this IQueryOver<T1, T1> query, List<string> list, Expression<Func<object>> expression,
            Func<QueryOverProjectionBuilder<T1>, QueryOverProjectionBuilder<T1>> selectList, int batchSize = 1000)
        {
            int i = 0;
            List<T1> result = new List<T1>();
            var queryOther = query.Clone();
            while (list.Count >= i * batchSize)
            {
                query = queryOther.Clone();
                var dt = list.Skip(i * batchSize).Take(batchSize).ToList();
                var batchResult = await query
                    .WhereRestrictionOn(expression).IsIn(dt)
                    .SelectList(selectList)
                    .TransformUsing(Transformers.AliasToBean<T1>())
                    .ListAsync();

                result.AddRange(batchResult);
                i++;
            }
            return result;
        }
        public static IQueryable<T1> ToQueryByListKey<T1>(this IQueryable<T1> query, List<string> list, string propertyName, int batchSize = 1000)
        {
            int i = 0;
            while (list.Count >= i * batchSize)
            {
                var dt = list.Skip(i * batchSize).Take(batchSize).ToList();
                query = query.Where<T1, string>(propertyName, c => dt.Contains(c));
                i++;
            }
            return query;
        }
    }
}
