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

        protected bool powerCopyPaste = false;
        public bool PowerCopyPaste { get { return powerCopyPaste; } }

        protected bool powerShapeshift = false;
        public bool PowerShapeshift { get { return powerShapeshift; } }

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();
            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;
        }

        #endregion

        #region Unlock Methods

        public void UnlockCopyPaste()
        {
            powerCopyPaste = true;
        }

        public void UnlockShapeshift()
        {
            powerShapeshift = true;
        }

        #endregion
    }
}