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
        protected float groundedVelocity = 1f;

        [SerializeField]
        protected float noGroundedVelocity = 0.5f;

        [SerializeField]
        private float groundedMovementInertia = 0.1f;

        [SerializeField]
        private float noGroundedMovementInertia = 0.5f;

        [SerializeField]
        private float maxVelocity = 2f;

        [SerializeField]
        private float minVelocity = 0.1f;

        private Vector3 velocity = Vector3.zero;
        public Vector3 Velocity { get { return velocity; } }

        [SerializeField]
        private float gravity = 10f;
        [SerializeField]
        private float noGroundedGravityVelocityCap = 20f;
        [SerializeField]
        private float groundedGravityVelocityCap = 20f;
        [SerializeField]
        private float groundedDistance = 0.05f;

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
                Vector3 inputMoveWithRotation = Quaternion.FromToRotation(Vector3.forward, player.CameraRotation.transform.forward) * (new Vector3(inputMove.x, 0, inputMove.y));
                //Vector3 inputMoveWithRotation = Quaternion.FromToRotation(Vector3.forward, transform.forward) * (new Vector3(inputMove.x, 0, inputMove.y));

                Vector3 velocityBuffer = velocity;
                Vector3 groundNormal = Vector3.up;

                // Calculate if grounded and update variables
                RaycastHit raycastHit;
                grounded = Physics.Raycast(transform.position + Vector3.up * groundedDistance, Vector3.down, out raycastHit, groundedDistance * 2);
                if (grounded && !jumping)
                {
                    // Apply inertia on velocity
                    velocity *= groundedMovementInertia;

                    // Set ground normal
                    groundNormal = raycastHit.normal;

                    // Apply ground normal rotation
                    inputMoveWithRotation = Quaternion.FromToRotation(Vector3.up, groundNormal) * inputMoveWithRotation;
                }
                else
                {
                    // Apply inertia on velocity but not y
                    velocity.x *= noGroundedMovementInertia;
                    velocity.y = Mathf.Min(0, velocity.y);
                    velocity.z *= noGroundedMovementInertia;
                }

                // Adding move velocity to velocity
                velocity += inputMoveWithRotation * ((grounded) ? groundedVelocity : noGroundedVelocity);

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

                // Adding gravity or jump velocity
                if (!jumpingHold)
                {
                    velocity.y += velocityBuffer.y - gravity * game.FixedDeltaTimeRun;
                    if(grounded)
                        velocity.y = Mathf.Max(velocity.y, -groundedGravityVelocityCap);
                    else
                        velocity.y = Mathf.Max(velocity.y, -noGroundedGravityVelocityCap);
                }
                else
                {
                    velocity.y += Mathf.Max(velocityBuffer.y, 0f);
                    velocity += Vector3.up * jumpVelocity * ((jumpTimeMax - (game.FixedTimeRun - jumpStartTime)) / jumpTimeMax).Sqr();
                }

                // Clamp X and Z movement velocity
                Vector2 movementVelocity = new Vector2(velocity.x, velocity.z);
                float movementVelocityMagnitude = movementVelocity.magnitude;
                if (movementVelocityMagnitude <= minVelocity)
                {
                    velocity.x = 0;
                    velocity.z = 0;
                }
                else if(movementVelocityMagnitude >= maxVelocity)
                {
                    velocity.x *= maxVelocity / movementVelocityMagnitude;
                    velocity.z *= maxVelocity / movementVelocityMagnitude;
                }

                // Apply velocity
                rigidbody.MovePosition(transform.position + velocity * game.FixedDeltaTimeRun);
            }
        }

        #endregion
    }
}