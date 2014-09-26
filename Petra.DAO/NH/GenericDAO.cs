using Petra.DAO.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petra.DAO.NH
{
    public class GenericDAO<T> : IDAO<T> where T : class
    {

        protected ISession Session
        {
            get { return NHibernateBase.Session; }
        }

        protected IStatelessSession StatelessSession
        {
            get { return NHibernateBase.StatelessSession; }
        }

        //private ISession session;

        public GenericDAO() {
            //session = NHibernateBase.Session;
        }

        public T GetById(int id) {
            return Session.Get<T>(id);
        }

        public void SaveOrUpdate(T entity, int Id)
        {
            try
            {
                try
                {
                    //NHibernateBase.BeginTransaction();
                    if (Id == 0)
                    {
                        Session.Save(entity);
                    }
                    else {
                        Session.Merge(entity);
                    }
                    //NHibernateBase.CommitTransaction();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                //NHibernateBase.CloseSession();
            }
        }
        
        public void Add(T entity)
        {
            try
            {
                try
                {
                    //NHibernateBase.BeginTransaction();
                    Session.Save(entity);
                    //NHibernateBase.CommitTransaction();
                }
                catch (Exception e) {
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                //NHibernateBase.CloseSession();
            }
        }

        public void Delete(T entity)
        {
            try
            {
                try
                {
                    NHibernateBase.BeginTransaction();
                    Session.Delete(entity);
                    NHibernateBase.CommitTransaction();
                }
                catch (Exception e)
                {
                    NHibernateBase.RollbackTransaction();
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                //NHibernateBase.CloseSession();
            }
        }

        public void Update(T entity)
        {
            try
            {
                try
                {
                    //NHibernateBase.BeginTransaction();
                    Session.Merge(entity);
                    //NHibernateBase.CommitTransaction();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                //NHibernateBase.CloseSession();
            }
        }

        public IEnumerable<T> All()
        {
            return Session.QueryOver<T>().List();
        }

        public void Dispose()
        {
            NHibernateBase.CloseSession();
        }
    }
}
