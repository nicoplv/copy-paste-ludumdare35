using UnityEngine;

namespace Controlers
{
	public class KeyboardMouse : Controler
	{
		#region Variables

		protected Games.Game game;

        protected bool playerJump = false;
        protected float playerJumpStartTime = -100f;

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();

            game = Games.Game.Instance;
        }

        public void Update()
        {
            bool playerJumpBuffer = playerJump;
            playerJump = Input.GetKey(KeyCode.Space);
            if (playerJump)
            {
                if (!playerJumpBuffer)
                    playerJumpStartTime = game.TimeRun;
            }
            else
                playerJumpStartTime = -100f;
        }

        #endregion

        #region Game Controler Methods

        public override Vector2 PlayerRotation()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public override Vector2 PlayerMove()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float playerMoveMagnitude = moveInput.magnitude;
            if (playerMoveMagnitude > 1)
                moveInput /= playerMoveMagnitude;
            return moveInput;
        }

        public override bool PlayerJump(out float _startTime)
        {
            _startTime = playerJumpStartTime;
            return playerJump;
        }

        #endregion

        #region Player Controler Methods

        public override bool PlayerShapeshift()
        {
            return Input.GetMouseButtonDown(0);
        }

        public override bool PlayerUnshapeshift()
        {
            return Input.GetMouseButtonDown(1);
        }

        #endregion
    }
}