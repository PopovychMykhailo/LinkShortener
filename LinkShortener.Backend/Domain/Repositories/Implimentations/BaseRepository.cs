using System;
using System.Collections.Generic;
using System.Linq;
using LinkShortener.Resource.Database;
using LinkShortener.Resource.Domain.Entities.Interfaces;
using LinkShortener.Resource.Domain.Repositories.Interfaces;

namespace LinkShortener.Resource.Domain.Repositories.Implimentations
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : EntityBase
    {
        private AppDbContext _Context { get; set; }



        public BaseRepository(AppDbContext context)
        {
            _Context = context;
        }


        public void Create(TDbModel model)
        {
            _Context.Set<TDbModel>().Add(model);
            _Context.SaveChanges();
        }

        public IList<TDbModel> GetAll()
        {
            return _Context.Set<TDbModel>().ToList();
        }

        public TDbModel GetById(Guid id)
        {
            return _Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }

        public void Update(TDbModel model)
        {
            TDbModel toUpdate = _Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
                toUpdate = model;

            _Context.Update(toUpdate);
            _Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            TDbModel toDelete = _Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            _Context.Set<TDbModel>().Remove(toDelete);
            _Context.SaveChanges();
        }
    }
}
