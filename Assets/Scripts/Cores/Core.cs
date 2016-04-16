using UnityEngine;

namespace Cores
{
    public class Core : DesignPatterns.Singleton<Core>
    {
        #region Singleton

        protected Core() { dontDestroyOnLoad = false; }

        #endregion
        
        #region Variables

        protected Games.Game game;
        protected Controlers.Controler controler;

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();
            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;
        }

        #endregion
    }
}