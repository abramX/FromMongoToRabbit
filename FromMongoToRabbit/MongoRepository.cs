using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;

namespace FromMongoToRabbit
{
    class MongoRepository:IDbRepository
    {
        private MongoClient _provider;
        private IMongoDatabase _db;
        string dbName;
        string dbCollection;
        public MongoRepository()
        {
            dbName = "mydb";
            dbCollection="products";
            var connectionString = "mongodb://localhost:27017";
            _provider = new MongoClient(connectionString);
            _db = _provider.GetDatabase(dbName);
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            var items = All<T>().Where(expression);
            foreach (T item in items)
            {
                Delete(item);
            }
        }
        public async void Delete<T>(T item) where T : class, new()
        {
            // Remove the object.
            await _db.GetCollection<T>(dbCollection).DeleteOneAsync(a=>a.Equals(item));
        }
        public void DeleteAll<T>() where T : class, new()
        {
            _db.DropCollection(typeof(T).Name);
        }
        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }
        public IQueryable<T> All<T>() where T : class, new()
        {
            return _db.GetCollection<T>(dbCollection).AsQueryable();
        }
   
        public async void Add<T>(T item) where T : class, new()
        {
            await _db.GetCollection<T>(dbCollection).InsertOneAsync(item);
        }
        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

    }
}
