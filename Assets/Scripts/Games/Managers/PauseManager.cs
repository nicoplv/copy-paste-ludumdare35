namespace Games.Managers
{
    public class PauseManager
    {
        #region Variables

        protected Game game;

        #endregion

        #region Constructor

        public PauseManager(Game _game)
        {
            game = _game;
        }

        #endregion

        #region Update Methods

        public void UpdateRun()
        {
            // TODO
            // Detect Pause
            // game.ToOnPause();
        }

        public void UpdateOnPause()
        {
            game.ToPause();
        }

        public void UpdatePause()
        {
            // TODO
            // Detect Resume
            // game.ToOnResume();
        }

        public void UpdateOnResume()
        {
            game.ToRun();
        }

        #endregion
    }
}