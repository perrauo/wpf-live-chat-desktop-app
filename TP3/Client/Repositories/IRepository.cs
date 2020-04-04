using System;
using System.Collections.Generic;
using System.Net.Http;
using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.NetworkFramework;
using System.Threading.Tasks;
 


namespace IFT585_TP3.Client.Repositories
{
    public interface IRepository<T>
    {
       
        void Connect(Connection connection);
        
        IModel Retrieve(object id);

        IEnumerable<T> RetrieveAll();

        void Update(T toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }

    public abstract class AbstractRepository<T>:IRepository<T>  where T:IModel 
    {
        protected Connection _connection;

        protected static HttpClient _client;

        public void Connect(Connection connection)
        {
            _connection = connection;
            if (_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(_connection.URL);
                _client.DefaultRequestHeaders.Accept.Clear();
            }
        }
        
        public virtual void Create(T obj)
        {
            var x = CreateTask(obj).GetAwaiter().GetResult();
            Console.WriteLine(x);
        }

        
        public virtual IModel Retrieve(object id)
        {
            return RetrieveTask(id).GetAwaiter().GetResult();
        }

        public virtual IEnumerable<T> RetrieveAll()
        {
            return RetrieveAllTask().GetAwaiter().GetResult();
        }

        public virtual void Update(T toUpdate)
        {
            var x = UpdateTask(toUpdate).GetAwaiter().GetResult();
            Console.WriteLine(x);
        }

        public virtual void Delete(object id)
        {
            var x = DeleteTask(id).GetAwaiter().GetResult();
            Console.WriteLine(x);
        }

        public virtual bool Exists(object id)
        {
            return ExistsTask(id).GetAwaiter().GetResult();
        }

        protected virtual async Task<Uri> CreateTask(T obj)
        {
            return null;
        }

        protected virtual async Task<T> RetrieveTask(object id)
        {
            return default;
        }

        protected virtual async Task<IEnumerable<T>> RetrieveAllTask()
        {
            return null;
        }

        protected virtual async Task<Uri> UpdateTask(T toUpdate)
        {
            return null;
        }

        protected virtual async Task<Uri> DeleteTask(object id)
        {
            return null;
        }

        protected virtual async Task<bool> ExistsTask(object id)
        {
            return false;
        }
    } 
}