using System;
using System.Collections.Generic;
using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.NetworkFramework;
using RestSharp;

namespace IFT585_TP3.Client.Repositories
{
    public interface IRepository<IModel>
    {
       
        void Connect(Connection connection);
        
        IModel Retrieve(object id);

        IEnumerable<IModel> RetrieveAll();

        void Update(IModel toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }

    public abstract class AbstractRepository<IModel>: IRepository<IModel>
    {
        private Connection _connection;

        public void Connect(Connection connection)
        {
            _connection = connection;
        }

        public virtual void Create(IModel obj)
        {
            
        }

        public abstract IModel Retrieve(object id);
        public abstract  IEnumerable<IModel> RetrieveAll();
        public abstract  void Update(IModel toUpdate);
        public abstract  void Delete(object id);
        public abstract  bool Exists(object id);
    } 
}