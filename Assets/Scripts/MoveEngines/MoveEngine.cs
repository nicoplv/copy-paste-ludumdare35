using UnityEngine;
using Games;

namespace MoveEngines
{
	public class MoveEngine : MonoBehaviour
	{
		#region Variables

		new protected Rigidbody rigidbody;
		protected Game game;

		private Quaternion rotation = Quaternion.identity;
		public Quaternion Rotation { get { return rotation; } }
		private Vector3 velocity = Vector3.zero;
		public Vector3 Velocity { get { return velocity; } }
		private bool grounded = false;
		public bool Grounded { get { return grounded; } }
		private MoveTypes moveType = MoveTypes.None;
		public MoveTypes MoveType { get { return moveType; } }

        private float speed = 0f;
        private Vector3 direction = Vector3.zero;

        [SerializeField]
		private float turnInertie = 0.1f;
		[SerializeField]
		private float directionInertie = 0.1f;
		[SerializeField]
		private float velocityInertie = 0.1f;

		[SerializeField]
		private float gravity = 10f;
		[SerializeField]
		private float gravityFrameCap = 0.25f;
		[SerializeField]
		private float groundedDistance = 0.05f;

		[SerializeField]
		private float velocityMinWalk = 0.1f;
        public bool Walk { get { return (velocity.sqrMagnitude > velocityMinWalk.Sqr()); } }
		[SerializeField]
		private float velocityMinRun = 1.0f;
        public bool Run { get { return (velocity.sqrMagnitude > velocityMinRun.Sqr()); } }

        #endregion

        #region Unity Methods

        public void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
			game = Game.Instance;

			// Initialize rotation
			rotation = transform.rotation;
		}

		public void FixedUpdate()
		{
			if (game.IsRun)
			{
				// Init move type for this loop
				moveType = MoveTypes.None;

				// Calculate the current rotation
				if (direction != Vector3.zero)
				{
					rotation = Quaternion.LookRotation(direction, Vector3.up);
					moveType = MoveTypes.Turn;
				}
				else
					rotation = transform.rotation; // May be remove

				// Apply rotation
				rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, rotation, game.DeltaTime / turnInertie));

				// Calculate average direction and velocity
				if (direction == Vector3.zero)
					direction = transform.forward;
				else
					direction = (direction.normalized + (transform.forward * directionInertie)) / (1f + directionInertie);
				velocity = velocity * velocityInertie;
				velocity += direction * speed;
				if (velocity.sqrMagnitude < (velocityMinWalk * velocityMinWalk))
					velocity = Vector3.zero;
				else if (speed < velocityMinRun)
					moveType = MoveTypes.Walk;
				else
					moveType = MoveTypes.Run;

				// Calculate grounded and gravity
				Vector3 gravityVector = Vector3.zero;
				Debug.DrawRay(transform.position + Vector3.up * groundedDistance, Vector3.down * groundedDistance * 2);
				grounded = Physics.Raycast(transform.position + Vector3.up * groundedDistance, Vector3.down, groundedDistance * 2);
				if (!grounded)
				{
					float gravityForce = game.DeltaTime * gravity;
					if (gravityForce > gravityFrameCap)
						gravityForce = gravityFrameCap;
					gravityVector = Vector3.down * gravityForce;
				}

				// Apply movement
				rigidbody.MovePosition(transform.position + (game.DeltaTime * velocity) + gravityVector);

                // Reset variables for next loop
                speed = 0f;
                direction = Vector3.zero;
            }
		}

        #endregion

        #region Class Methods

        public void Move(float _speed, Vector3 _direction)
        {
            speed = _speed;
            direction = _direction;
        }

        #endregion
    }
}