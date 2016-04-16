using UnityEngine;
using Games.Managers;

namespace Games
{
    public class Game : DesignPatterns.Singleton<Game>
    {
        #region Constructor

        protected Game() { dontDestroyOnLoad = false; }

        #endregion

        #region Variables

        // State of the game
        protected States state = States.Awake;
        public States State { get { return state; } }

        // Managers
        protected StartManager startManager;
        public StartManager StartManager { get { return startManager; } }
        protected PauseManager pauseManager;
        public PauseManager PauseManager { get { return pauseManager; } }
        protected DoneManager doneManager;
        public DoneManager DoneManager { get { return doneManager; } }

        // Default time
        protected float time = 0;
        public float Time { get { return time; } }
        protected float deltaTime = 0;
        public float DeltaTime { get { return deltaTime; } }
        protected float fixedTime = 0;
        public float FixedTime { get { return fixedTime; } }
        protected float fixedDeltaTime = 0;
        public float FixedDeltaTime { get { return fixedDeltaTime; } }

        // Run time
        protected float timeRun = 0;
        public float TimeRun { get { return timeRun; } }
        protected float deltaTimeRun = 0;
        public float DeltaTimeRun { get { return deltaTimeRun; } }
        protected float fixedTimeRun = 0;
        public float FixedTimeRun { get { return fixedTimeRun; } }
        protected float fixedDeltaTimeRun = 0;
        public float FixedDeltaTimeRun { get { return fixedDeltaTimeRun; } }

        #endregion

        #region Getter and Setter

        public bool IsOnAwake { get { return state == States.Awake; } }
        public bool IsOnStart { get { return state == States.OnStart; } }
        public bool IsOnResume { get { return state == States.OnResume; } }
        public bool IsRun { get { return state == States.Run; } }
        public bool IsOnPause { get { return state == States.OnPause; } }
        public bool IsPause { get { return state == States.Pause; } }
        public bool IsOnDone { get { return state == States.OnDone; } }
        public bool IsDone { get { return state == States.Done; } }

        public void ToAwake() { state = States.Awake; }
        public void ToOnStart() { state = States.OnStart; }
        public void ToOnResume() { state = States.OnResume; }
        public void ToRun() { state = States.Run; }
        public void ToOnPause() { state = States.OnPause; }
        public void ToPause() { state = States.Pause; }
        public void ToOnDone() { state = States.OnDone; }
        public void ToDone() { state = States.Done; }

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();

            // Create the managers
            startManager = new StartManager(this);
            pauseManager = new PauseManager(this);
            doneManager = new DoneManager(this);
        }

        public void Update()
        {
            // Update default time
            deltaTime = UnityEngine.Time.deltaTime;
            time += deltaTime;

            if (state == States.Run)
            {
                deltaTimeRun = UnityEngine.Time.deltaTime;
                timeRun += deltaTimeRun;
            }
            else
                deltaTimeRun = 0;

            // Update managers when is necessary
            switch (state)
            {
                case States.Awake:
                    startManager.UpdateAwake();
                    break;

                case States.OnStart:
                    startManager.UpdateOnStart();
                    break;

                case States.OnResume:
                    pauseManager.UpdateOnResume();
                    break;

                case States.Run:
                    pauseManager.UpdateRun();
                    doneManager.UpdateRun();
                    break;

                case States.OnPause:
                    pauseManager.UpdateOnPause();
                    break;

                case States.Pause:
                    pauseManager.UpdatePause();
                    break;

                case States.OnDone:
                    doneManager.UpdateOnDone();
                    break;
            }
        }

        public void FixedUpdate()
        {
            // Update fixed time
            fixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
            fixedTime += fixedDeltaTime;

            if (state == States.Run)
            {
                fixedDeltaTimeRun = UnityEngine.Time.fixedDeltaTime;
                fixedTimeRun += fixedDeltaTimeRun;
            }
            else
                fixedDeltaTimeRun = 0;
        }

        #endregion
    }
}