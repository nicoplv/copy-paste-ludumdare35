namespace Games.Managers
{
    public class StartManager
    {
        #region Variables

        protected Game game;

        #endregion

        #region Constructor

        public StartManager(Game _game)
        {
            game = _game;
        }

        #endregion

        #region Update Methods

        public void UpdateAwake()
        {
            game.ToOnStart();
        }

        public void UpdateOnStart()
        {
            game.ToRun();
        }

        #endregion
    }
}