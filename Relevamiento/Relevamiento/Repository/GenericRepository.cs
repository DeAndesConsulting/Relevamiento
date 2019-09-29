using Relevamiento.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relevamiento.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        public bool Insert(T obj)
        {
            using (SQLite.SQLiteConnection db = new SQLiteConnection(App.RutaBD))
            {
                return db.Insert(obj) > 0 ? true : false;
            }
        }

        public bool Delete(T obj)
        {
            using (SQLite.SQLiteConnection db = new SQLiteConnection(App.RutaBD))
            {
                return db.Delete(obj) > 0 ? true : false;
            }
        }

        public T GetById(int id)
        {
            using (SQLite.SQLiteConnection db = new SQLiteConnection(App.RutaBD))
            {
                return db.Get<T>(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (SQLite.SQLiteConnection db = new SQLiteConnection(App.RutaBD))
            {
                return db.Table<T>();
            }
        }

        public bool Update(T obj)
        {
            using (SQLite.SQLiteConnection db = new SQLiteConnection(App.RutaBD))
            {
                return db.Update(obj) > 0 ? true : false;
            }
        }
    }
}
