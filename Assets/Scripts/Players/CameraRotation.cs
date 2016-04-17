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

            // Init camera look at
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

                // Manage camera distance
                float cameraDistanceMax = player.CurrentShapeshiftableObject.CameraDistance;
                float cameraDistanceMin = player.CurrentShapeshiftableObject.CameraDistance / 3f;
                RaycastHit[] raycastHits = Physics.RaycastAll(lookAt.position, - secondRotation.transform.forward, cameraDistanceMax, Statics.Layers.Collider.Mask);
                if(raycastHits.Length > 0)
                {
                    foreach (RaycastHit iRaycastHit in raycastHits)
                    {
                        // Check if not same object collision
                        bool sameObjectCollision = false;
                        foreach (Collider iCollider in player.CurrentShapeshiftableObject.CollisionCollider)
                        {
                            if (iCollider == iRaycastHit.collider)
                            {
                                sameObjectCollision = true;
                                break;
                            }
                        }
                        if (!sameObjectCollision)
                        {
                            float distanceBuffer = (iRaycastHit.point - lookAt.position).magnitude;
                            if (distanceBuffer < cameraDistanceMax)
                                cameraDistanceMax = distanceBuffer;
                        }
                    }
                }

                if (cameraDistanceMax < cameraDistanceMin)
                {
                    Vector3 minDisplacement = secondRotation.transform.forward;
                    minDisplacement.y = 0;
                    camera.transform.position = lookAt.position - minDisplacement * cameraDistanceMin;
                    //cameraDistanceMax = cameraDistanceMin;
                }
                else
                {
                    camera.transform.localPosition = new Vector3(0, 0, -cameraDistanceMax);

                    // Update camera look at
                    camera.transform.LookAt(lookAt);
                }

                }
        }

        #endregion
    }
}