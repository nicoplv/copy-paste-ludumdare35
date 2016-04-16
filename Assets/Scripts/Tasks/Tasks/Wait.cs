using System.Collections;

namespace Tasks
{
    public class Wait : AbstractTask
    {
        #region Variables

        protected Games.Game game;

        protected float duration;
        protected float startTime;

        protected Type type;

        #endregion

        #region Enum

        public enum Type
        {
            Time,
            FixedTime,
            TimeRun,
            FixedTimeRun,
        }

        #endregion

        #region Constructor

        public Wait(Stack _stack, float _duration, Type _type)
            : base(_stack)
        {
            game = Games.Game.Instance;
            stack = _stack;
            duration = _duration;
            type = _type;
        }

        #endregion

        #region Class Methods

        public override void Start()
        {
            inProgress = true;

            // Init time
            startTime = GetTime();

            // Start coroutine to detect when this task is ended
            Coroutine.Start(EndTask());
        }

        protected IEnumerator EndTask()
        {
            while (startTime + duration > GetTime())
                yield return null;

            inProgress = false;

            // Start next task
            stack.Next();
        }

        protected float GetTime()
        {
            switch (type)
            {
                case Type.Time:
                    return game.Time;
                case Type.FixedTime:
                    return game.FixedTime;
                case Type.TimeRun:
                    return game.TimeRun;
                case Type.FixedTimeRun:
                    return game.FixedTimeRun;
            }
            return 0f;
        }

        #endregion
    }
}