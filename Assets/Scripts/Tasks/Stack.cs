using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class Stack
    {
        #region Variables

        protected List<AbstractTask> tasks = new List<AbstractTask>();

        protected int currentTask = -1;

        protected bool started = false;
        protected bool ended = false;

        #endregion

        #region Add Methods

        public void AddTask(Task.Method _method, Task.IsEnded _isEnded)
        {
            tasks.Add(new Task(this, _method, _isEnded));
            AutoRestart();
        }

        public void AddTask(Task.Method _method)
        {
            AddTask(_method, () => { return true; });
        }

        public void AddWait(float _duration, Wait.Type _type)
        {
            tasks.Add(new Wait(this, _duration, _type));
            AutoRestart();
        }

        #endregion

        #region Methods

        public void Start()
        {
            started = true;

            // Start first task
            Next();
        }

        protected void AutoRestart()
        {
            if (started && ended)
            {
                ended = false;

                // Start next task
                Next();
            }
        }

        public void Next()
        {
            // Check if a next task exist
            if (currentTask + 1 < tasks.Count)
            {
                currentTask++;

                // Start the next task
                tasks[currentTask].Start();
            }
            // Else define the stack ended
            else
                ended = true;
        }

        #endregion
    }
}