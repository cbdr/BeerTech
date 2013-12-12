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