using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace PearAdmin.AbpTemplate.BinaryObjects
{
    public class BinaryObjectManager : IBinaryObjectManager, ITransientDependency
    {
        private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;

        public BinaryObjectManager(IRepository<BinaryObject, Guid> binaryObjectRepository)
        {
            _binaryObjectRepository = binaryObjectRepository;
        }

        public Task<BinaryObject> GetOrNullAsync(Guid id)
        {
            return _binaryObjectRepository.FirstOrDefaultAsync(id);
        }

        public Task SaveAsync(BinaryObject file)
        {
            return _binaryObjectRepository.InsertAsync(file);
        }

        public Task DeleteAsync(Guid id)
        {
            return _binaryObjectRepository.DeleteAsync(id);
        }
    }
}