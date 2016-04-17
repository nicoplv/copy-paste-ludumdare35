using UnityEngine;

namespace Players
{
    public class CameraRotation : MonoBehaviour
    {
        #region Variables

        protected Games.Game game;
        protected Controlers.Controler controler;

        [SerializeField]
        protected Player player;
        public Player Player { get { return player; } }

        [SerializeField]
        protected new Camera camera;
        public Camera Camera { get { return camera; } }

        [SerializeField]
        protected Transform secondRotation;

        [SerializeField]
        protected Transform lookAt;
        public Transform LookAt { get { return lookAt; } set { lookAt = value; } }

        [SerializeField]
        protected Vector2 limitY = new Vector2(-60f, 60f);

        [SerializeField]
        protected float sensitivityX = 1f;
        [SerializeField]
        protected float sensitivityY = 1f;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;

            // Init camera position and look at
            camera.transform.LookAt(lookAt);
        }

        public void Update()
        {
            if (game.IsRun)
            {
                // Get rotation input
                Vector2 inputRotation = controler.PlayerRotation();

                if (inputRotation.x != 0)
                    transform.rotation *= Quaternion.AngleAxis(inputRotation.x * sensitivityX, Vector3.up);

                if (inputRotation.y != 0)
                {
                    float rotationY = (secondRotation.localEulerAngles.x - inputRotation.y * sensitivityY).NormalizeAngle();
                    rotationY = Mathf.Clamp(rotationY, limitY.x, limitY.y);
                    secondRotation.localEulerAngles = new Vector3(rotationY, 0, 0);
                }

                // Update camera position and look at
                camera.transform.LookAt(lookAt);
            }
        }

        #endregion
    }
}