using UnityEngine;

namespace Players
{
    public class Player : DesignPatterns.Singleton<Player>
    {
        #region Constructor

        protected Player() { dontDestroyOnLoad = false; }

        #endregion

        #region Variables

        [SerializeField]
        protected Movement movement;
        public Movement Movement { get { return movement; } }
        
        [SerializeField]
        protected CameraRotation cameraRotation;
        public CameraRotation CameraRotation { get { return cameraRotation; } }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        #endregion
    }
}