using UnityEngine;

namespace Controlers
{
	public class KeyboardMouse : Controler
	{
		#region Variables

		protected Games.Game game;

        protected bool mouseButton1 = false;
        protected bool mouseButton2 = false;

        protected Vector2 mouseInput = Vector2.zero;

        protected Vector3 moveInput = Vector3.zero;

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
            mouseButton1 = Input.GetMouseButton(1);
            mouseButton2 = Input.GetMouseButton(2);

            mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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
            return mouseInput;
        }

        public override Vector2 PlayerMove()
        {
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

        #endregion
    }
}