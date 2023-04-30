using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims
{
    /// <summary>
    /// Wrapper for Inventor's undo transactions. This provides a simple way to wrap an undo transaction within
    /// a try/catch/finally block. The Try, Catch, and Finally actions are all optional. 
    /// </summary>
    public class UndoTransaction
    {
        private readonly Inventor.Application application;
        private readonly _Document document;
        private readonly string transactionMessage;

        /// <summary>
        /// A user-specified Action in the Try body of the transaction.
        /// </summary>
        public Action Try;

        /// <summary>
        /// A user-specified Action in the Catch body of the transaction.
        /// </summary>
        public Action Catch;

        /// <summary>
        /// A user-specified Action in the Finally body of the transaction.
        /// </summary>
        public Action Finally;

        /// <summary>
        /// Creates a new UndoTransaction object.
        /// </summary>
        /// <param name="_application">Current Inventor Application object.</param>
        /// <param name="transactionMessage">The name of the transaction as a string.</param>
        public UndoTransaction(Inventor.Application _application, string transactionMessage)
        {
            this.application = _application;
            document = _application.ActiveDocument;
            this.transactionMessage = transactionMessage;
        }

        /// <summary>
        /// Kicks off the UndoTranaction series of operations.
        /// </summary>
        public void Invoke()
        {
            Transaction transaction = application.TransactionManager
                .StartTransaction(document, transactionMessage);

            try
            {
                Try?.Invoke();
                transaction.End();
            }
            catch
            {
                transaction.Abort();
                Catch?.Invoke();
            }
            finally
            {
                Finally?.Invoke();
            }

            transaction = null;
            document = null;
            application = null;
        }
    }
}
