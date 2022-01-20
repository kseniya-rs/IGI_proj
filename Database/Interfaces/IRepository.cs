using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IRepository<TModel>
    {
        void Create(TModel item);

        TModel GetItem(int id);

        List<TModel> GetItems();

        void Update(TModel item);

        void Remove(int id);
    }
}
