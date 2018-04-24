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
    public class MongoRepository:IDbRepository
    {
        private MongoClient _provider;
        private IMongoDatabase _db;
        string _dbName;
        string _dbCollection;
        string _connectionString;
        public MongoRepository(string connectionString, string dbName,string dbCollection)
        {
            _dbName = dbName;
            _dbCollection= dbCollection;
            _connectionString = connectionString;
            _provider = new MongoClient(_connectionString);
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
            await _db.GetCollection<T>(_dbCollection).DeleteOneAsync(a=>a.Equals(item));
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
            return _db.GetCollection<T>(_dbCollection).AsQueryable();
        }  
        public async void Add<T>(T item) where T : class, new()
        {
             await _db.GetCollection<T>(_dbCollection).InsertOneAsync(item);
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
