using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BeerTech.Repository
{
    public class BaseRepository <T>
    {

        protected ISession Session { get; set; }

        public BaseRepository()
        {
            Session = CreateSession();
        }

        public void Save(T Item)
        {
            Session.BeginTransaction();
            Session.Save(Item);
            Session.Transaction.Commit();
        }

        public void SaveOrUpdate(T Item)
        {
            Session.BeginTransaction();
            Session.SaveOrUpdate(Item);
            Session.Transaction.Commit();
        }

        public void Update(T Item)
        {
            Session.BeginTransaction();
            Session.Update(Item);
            Session.Transaction.Commit();
        }

        public T LoadByKey(string key)
        {
            return Session.Get<T>(key);
        }

        private ISession CreateSession()
        {
            var factory = CreateSessionFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            return factory.OpenSession();
        }

        private ISessionFactory CreateSessionFactory(string connectionString)
        {
            var dbconn = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
            dbconn.AdoNetBatchSize(0);
            return Fluently.Configure().Database(dbconn)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                .BuildSessionFactory();
        }

    }
}