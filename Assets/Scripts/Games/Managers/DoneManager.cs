namespace Games.Managers
{
    public class DoneManager
    {
        #region Variables

        protected Game game;

        #endregion

        #region Constructor

        public DoneManager(Game _game)
        {
            game = _game;
        }

        #endregion

        #region Update Methods

        public void UpdateRun()
        {
            // TODO
            // Detect Done
            // game.ToOnDone();
        }

        public void UpdateOnDone()
        {
            game.ToDone();
        }

        #endregion
    }
}