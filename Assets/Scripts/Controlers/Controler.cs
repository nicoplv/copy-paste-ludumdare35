using UnityEngine;
using System.Collections;

namespace Controlers
{
	public class Controler : DesignPatterns.Singleton<Controler>
	{
		#region Constructor

		protected Controler() { dontDestroyOnLoad = false; }

		#if UNITY_STANDALONE
			public new static void CreateInstance()
			{
				instance = singletonGameObject.AddComponent<KeyboardMouse>();
			}
		#endif

		#endregion

		#region Game Controler Methods

		public virtual bool GamePause()
		{
			return false;
		}

		#endregion

		#region Player Controler Methods

		public virtual bool PlayerMove(out Vector3 _moveVector)
		{
			_moveVector = Vector3.zero;
			return false;
		}

		public virtual bool PlayerJump()
		{
			return false;
        }

        public virtual bool PlayerCopy()
        {
            // TO DO
            // Add out parameter object

            return false;
        }

        public virtual bool PlayerPaste()
        {
            // TO DO
            // Add out parameter object

            return false;
        }

        public virtual bool PlayerShapeshift()
        {
            // TO DO
            // Add out parameter object

            return false;
        }

        #endregion

    }
}