using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class BaseRepository<TModel> : IRepository<TModel> where TModel: class
    {
        protected DatabaseContext Context;
        protected DbSet<TModel> DbSet;

        public BaseRepository(DatabaseContext context)
        {
            Context = context;
            DbSet = Context.Set<TModel>();
        }

        public void Create(TModel item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var item = DbSet.Find(id);
            if (item != null)
                DbSet.Remove(item);
            Context.SaveChanges();
        }

        public void Update(TModel item)
        {
            Context.Entry<TModel>(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public TModel GetItem(int id)
        {
            return DbSet.Find(id);
        }

        public List<TModel> GetItems()
        {
            return DbSet.ToList(); // можно ли просто ЕНумерабле?
        }
    }
}
