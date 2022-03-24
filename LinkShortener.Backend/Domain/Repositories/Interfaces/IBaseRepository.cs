using System;
using System.Collections.Generic;
using LinkShortener.Resource.Domain.Entities.Interfaces;

namespace LinkShortener.Resource.Domain.Repositories.Interfaces
{
    public interface IBaseRepository<TDbModel> where TDbModel : EntityBase
    {
        public void Create(TDbModel model);
        public IList<TDbModel> GetAll();
        public TDbModel GetById(Guid id);
        public void Update(TDbModel model);
        public void Delete(Guid id);
    }
}
