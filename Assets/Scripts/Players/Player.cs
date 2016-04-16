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

        [SerializeField]
        protected Gaze gaze;
        public Gaze Gaze { get { return gaze; } }

        [SerializeField]
        protected ShapeshiftableObjects.ShapeshiftableObject defaultShapeshiftableObject;

        [SerializeField]
        protected ShapeshiftableObjects.ShapeshiftableObject currentShapeshiftableObject;
        public ShapeshiftableObjects.ShapeshiftableObject CurrentShapeshiftableObject { get { return currentShapeshiftableObject; } }

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();

            // Initialize shapeshift
            Unshapeshift();
        }

        #endregion

        #region Shapeshift Methods

        public void ShapeshiftTo(ShapeshiftableObjects.ShapeshiftableObject _shapeshiftableObject)
        {
            // Buffer the rotation
            Quaternion rotationBuffer = currentShapeshiftableObject.transform.rotation;

            // Remove current object
            Destroy(currentShapeshiftableObject.gameObject);

            // Instantiate new object
            currentShapeshiftableObject = Instantiate(Resources.Load<GameObject>(_shapeshiftableObject.Resource)).GetComponent<ShapeshiftableObjects.ShapeshiftableObject>();
            currentShapeshiftableObject.transform.SetParent(transform, false);

            // Disable inview collider
            currentShapeshiftableObject.DisableInView();
        }

        public void Unshapeshift()
        {
            ShapeshiftTo(defaultShapeshiftableObject);
        }

        #endregion
    }
}