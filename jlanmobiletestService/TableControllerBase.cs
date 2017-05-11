using jlanmobiletestService.DataObjects;
using jlanmobiletestService.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace jlanmobiletestService
{
    public abstract class TableControllerBase<TData> 
        : TableController<TData> where TData : class, ITableData
    {
        protected async Task DeleteAsync<T>(string id, Func<TData, ICollection<T>> func) 
            where T : class, ITableData
        {
            await DeleteAsync(id);

            var edm = DomainManager as EntityDomainManager<TData>;
            if (edm != null && edm.EnableSoftDelete)
            {
                var context = edm.Context;
                if (context != null)
                {
                    var obj = await context.Set<TData>()
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (obj != null)
                    {
                        foreach (var relation in func.Invoke(obj))
                        {
                            relation.Deleted = true;
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

        protected async Task DeleteAsync<T, K>(string id, Func<TData, ICollection<T>> funcT, Func<TData, ICollection<T>> funcK)
            where T : class, ITableData
            where K : class, ITableData
        {
            await DeleteAsync(id);

            var edm = DomainManager as EntityDomainManager<TData>;
            if (edm != null && edm.EnableSoftDelete)
            {
                var context = edm.Context;
                if (context != null)
                {
                    var obj = await context.Set<TData>()
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (obj != null)
                    {
                        foreach (var relation in funcT.Invoke(obj))
                        {
                            relation.Deleted = true;
                        }

                        foreach (var relation in funcK.Invoke(obj))
                        {
                            relation.Deleted = true;
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

    }
}