using UnityEngine;

namespace Players
{
    public class Movement : MonoBehaviour
    {
        #region Variables

        protected Games.Game game;
        protected Controlers.Controler controler;

        [SerializeField]
        protected Player player;
        public Player Player { get { return player; } }

        [SerializeField]
        protected new Rigidbody rigidbody;
        public Rigidbody Rigidbody { get { return rigidbody; } }

        [SerializeField]
        protected float velocityFactor = 1f;

        [SerializeField]
        protected float velocityClamp = 5f;

        [SerializeField]
        protected float rotationInertia = 5f;

        [SerializeField]
        private float gravity = 10f;

        private bool grounded = false;
        public bool Grounded { get { return grounded; } }

        [SerializeField]
        protected float jumpCooldown = 0.25f;
        [SerializeField]
        protected float jumpTimeMax = 0.25f;
        [SerializeField]
        protected float jumpVelocity = 1f;

        protected bool jumping = false;
        protected bool jumpingHold = false;
        protected float jumpStartTime = -100f;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;
        }

        public void FixedUpdate()
        {
            if (game.IsRun)
            {
                // Get move input
                Vector2 inputMove = controler.PlayerMove();
                Vector3 inputMoveWithRotation = Quaternion.FromToRotation(Vector3.forward, player.CurrentShapeshiftableObject.transform.forward) * (new Vector3(inputMove.x, 0, inputMove.y));
                inputMoveWithRotation.y = 0;

                // Rotate player
                if (inputMove != Vector2.zero)
                {
                    Vector3 cameraForward = player.CameraRotation.transform.forward;
                    cameraForward.y = 0;
                    Quaternion lookRotation = Quaternion.LookRotation(cameraForward);
                    player.CurrentShapeshiftableObject.transform.rotation = Quaternion.Slerp(player.CurrentShapeshiftableObject.transform.rotation, lookRotation, rotationInertia * game.DeltaTimeRun);
                }

                // Calculate target velocity on x and z
                Vector3 targetVelocity = inputMoveWithRotation * velocityFactor / rigidbody.mass / game.DeltaTimeRun;

                // Calculate change velocity on x and z
                Vector3 movementVelocityBuffer = rigidbody.velocity;
                movementVelocityBuffer.y = 0;
                Vector3 changeVelocity = (targetVelocity - movementVelocityBuffer);
                changeVelocity.y = 0;

                // Clamp velocity
                float magnitude = changeVelocity.magnitude;
                if (magnitude > velocityClamp)
                    changeVelocity = changeVelocity / magnitude * velocityClamp;

                // Add change velocity force
                if (changeVelocity != Vector3.zero)
                rigidbody.AddForce(changeVelocity, ForceMode.VelocityChange);

                // Check jump
                float inputJumpStartTime;
                if (controler.PlayerJump(out inputJumpStartTime))
                {
                    jumpingHold = true;
                    if (!jumping && grounded && (game.FixedTimeRun - inputJumpStartTime < jumpCooldown))
                    {
                        jumping = true;
                        jumpStartTime = game.FixedTimeRun;
                    }
                    if (jumpingHold && (game.FixedTimeRun - jumpStartTime > jumpTimeMax))
                    {
                        jumpingHold = false;
                        jumpStartTime = -100f;
                    }
                }
                else
                {
                    jumpingHold = false;
                    jumpStartTime = -100f;
                    if (grounded)
                        jumping = false;
                }

                // Apply jump velocity
                if (jumpingHold)
                    rigidbody.AddForce(Vector3.up * jumpVelocity * ((jumpTimeMax - (game.FixedTimeRun - jumpStartTime)) / jumpTimeMax).Sqr() / Mathf.Sqrt(rigidbody.mass), ForceMode.VelocityChange);

                // Apply gravity
                rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

                // Reset grouded
                grounded = false;
            }
        }

        public void OnCollisionStay(Collision _collision)
        {
            foreach (ContactPoint iContact in _collision.contacts)
            {
                if (Vector3.Angle(transform.up, iContact.normal) < 5f)
                    grounded = true;
            }
        }
        
        #endregion
    }
}