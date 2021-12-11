using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class AbpTemplateRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<AbpTemplateDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected AbpTemplateRepositoryBase(IDbContextProvider<AbpTemplateDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
