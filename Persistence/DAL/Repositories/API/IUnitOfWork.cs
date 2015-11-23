using System;

namespace DAL.Repositories.API
{
    public interface IUnitOfWork
    {

        void Commit();

        /// <summary>
        /// Performs an action within a transaction scope
        /// </summary>
        /// <param name="scoped">The action is wrapped up in the Transaction Scope.
        /// The first Action in the Action's generic parameter is the commit delegate.
        /// The second Action is the rollback delegate.</param>
        void InScope(Action<IUnitOfWork,Action, Action> scopedActionWithRollback);

        /// <summary>
        /// Performs an action within a transaction scope in a try catch block
        /// </summary>
        /// <param name="scopedAction">The action that wrapped up in the Transaction Scope </param>
        /// <param name="onException">This is invoked upon exception. The first parameter is the rollback delegate</param>
        void TryInScope(Action<IUnitOfWork> scopedAction, Action<Action, Exception> onException);
    }
}
  
