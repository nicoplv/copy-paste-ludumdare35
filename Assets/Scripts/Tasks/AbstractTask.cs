using System.Collections;

namespace Tasks
{
    public abstract class AbstractTask
    {
        #region Variables

        protected Stack stack;

        protected bool inProgress = false;
        public bool InProgress { get { return inProgress; } }

        #endregion

        #region Constructor

        public AbstractTask(Stack _stack)
        {
            stack = _stack;
        }

        #endregion

        #region Class Methods

        public abstract void Start();

        #endregion
    }
}