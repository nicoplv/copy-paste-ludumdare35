using System.Collections;

namespace Tasks
{
    public class Task : AbstractTask
    {
        #region Variables

        protected Method method;

        protected IsEnded isEnded;

        #endregion

        #region Deleguate

        public delegate void Method();
        public delegate bool IsEnded();

        #endregion

        #region Constructor

        public Task(Stack _stack, Method _method, IsEnded _isEnded)
            : base (_stack)
        {
            method = _method;
            isEnded = _isEnded;
        }

        #endregion

        #region Class Methods

        public override void Start()
        {
            inProgress = true;

            // Execute method
            method();
            
            // Start coroutine to detect when this task is ended
            Coroutine.Start(EndTask());
        }

        protected IEnumerator EndTask()
        {
            while (!isEnded())
                yield return null;

            inProgress = false;

            // Start next task
            stack.Next();
        }

        #endregion
    }
}