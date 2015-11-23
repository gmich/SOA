using DAL.Repositories.API;
using System;
using System.Data.Entity;

namespace DAL.Repositories
{
    internal class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void InScope(Action<IUnitOfWork, Action, Action> scopedActionWithRollback)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                scopedActionWithRollback(
                    this,
                    () => dbContextTransaction.Commit(),
                    () => dbContextTransaction.Rollback());

            }
        }

        public void TryInScope(Action<IUnitOfWork> scopedAction, Action<Action, Exception> onException)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    scopedAction(this);
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    onException(() => dbContextTransaction.Rollback(), ex);
                }
            }
        }
    }
}