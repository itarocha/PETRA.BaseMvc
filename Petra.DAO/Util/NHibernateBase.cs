using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;

namespace Petra.DAO.Util
{
    public class NHibernateBase
    {
        private static ISession session = null;

        private static Configuration Configuration { get; set; }
        protected static ISessionFactory SessionFactory { get; set; }
        private static IStatelessSession statelessSession = null;

        public static Configuration ConfigureNHibernate(System.Reflection.Assembly a)
        {
            var Configuration = new NHibernate.Cfg.Configuration();
            Configuration.Configure(); // read config default style


                //Configuration.AddAssembly(a);
            SessionFactory = Fluently.Configure(Configuration)
                    .Mappings(
                      m => m.FluentMappings.AddFromAssembly(a))
                    .BuildSessionFactory();

            //SessionFactory = Configuration.BuildSessionFactory();

            return Configuration; // Configuration;
        }


        public static Configuration ConfigureNHibernate(System.Reflection.Assembly[] assemblies ) {
            /*
            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(); // read config default style
            Fluently.Configure(cfg)
                .Mappings(
                  m => m.FluentMappings.AddFromAssemblyOf<Turma>())
                .BuildSessionFactory();
            */

            /*
            Configuration = new Configuration();
            Configuration.Configure();
             */
            var Configuration = new NHibernate.Cfg.Configuration();
            Configuration.Configure(); // read config default style

            foreach (System.Reflection.Assembly a in assemblies) {
                //Configuration.AddAssembly(a);
                Fluently.Configure(Configuration)
                    .Mappings(
                      m => m.FluentMappings.AddFromAssembly(a))
                    .BuildSessionFactory();

            }





            SessionFactory = Configuration.BuildSessionFactory();

            return Configuration; // Configuration;
        }
        
        public static ISession OpenSession()
        {
            return session = SessionFactory.OpenSession();
        }
        
        public static ISession Session {
            get {
                if  (session == null) {
                    session = SessionFactory.OpenSession();
                }
                return session;
             }
        }

        public static IStatelessSession StatelessSession {
            get {
                if (statelessSession == null) {
                    statelessSession = SessionFactory.OpenStatelessSession();
                }
                return statelessSession;
            }
        }

        public static void BeginTransaction() {
            Session.BeginTransaction();
        }

        public static void CloseSession() {
            if ((session != null) && (session.IsOpen)) {
                session.Close();
            }
            if (session != null)
            {
                session = null;
            }
        }

        public static void CommitTransaction()
        {
            Session.Transaction.Commit();
        }

        public static void RollbackTransaction()
        {
            Session.Transaction.Rollback();
        }
    }
}
