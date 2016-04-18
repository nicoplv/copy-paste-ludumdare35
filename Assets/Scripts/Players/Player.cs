using UnityEngine;

namespace Players
{
    public class Player : DesignPatterns.Singleton<Player>
    {
        #region Constructor

        protected Player() { dontDestroyOnLoad = false; }

        #endregion

        #region Variables

        protected Games.Game game;
        protected Controlers.Controler controler;

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
        protected ShapeshiftableObject defaultShapeshiftableObject;

        [SerializeField]
        protected ShapeshiftableObject currentShapeshiftableObject;
        public ShapeshiftableObject CurrentShapeshiftableObject { get { return currentShapeshiftableObject; } }

        [SerializeField]
        protected float eyesLookAtAngle = 100f;

        [SerializeField]
        protected float eyesRotationInertia = 5f;

        [SerializeField]
        protected GameObject leftEye;

        [SerializeField]
        protected GameObject rightEye;

        #endregion

        #region Unity Methods

        public new void Awake()
        {
            base.Awake();

            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;

            // Hide the cursor
            Cursor.visible = false;

            // Initialize shapeshift
            Unshapeshift();
        }

        public void Update()
        {
            if (game.IsRun)
            {
                // Exit button
                if (controler.GameQuit())
                    Application.Quit();

                // Update eyes position
                leftEye.transform.position = currentShapeshiftableObject.LeftEye.position;
                rightEye.transform.position = currentShapeshiftableObject.RightEye.position;

                // Update eyes scale
                leftEye.transform.localScale = currentShapeshiftableObject.LeftEye.localScale;
                rightEye.transform.localScale = currentShapeshiftableObject.RightEye.localScale;

                // Update eyes rotation with look camera
                Quaternion leftEyeRotation = currentShapeshiftableObject.LeftEye.rotation; ;
                Quaternion rightEyeRotation = currentShapeshiftableObject.RightEye.rotation; ;
                Vector3 currentShapeshiftableObjectXYForward = currentShapeshiftableObject.transform.forward;
                currentShapeshiftableObjectXYForward.y = 0;
                Vector3 cameraXYForward = CameraRotation.Camera.transform.forward;
                cameraXYForward.y = 0;
                if (Vector3.Angle(currentShapeshiftableObjectXYForward, cameraXYForward) > eyesLookAtAngle)
                {
                    Quaternion lookRotationLeftEye = Quaternion.LookRotation(CameraRotation.Camera.transform.position - leftEye.transform.position);
                    Quaternion lookRotationRightEye = Quaternion.LookRotation(CameraRotation.Camera.transform.position - rightEye.transform.position);
                    leftEyeRotation = lookRotationLeftEye;
                    rightEyeRotation = lookRotationRightEye;
                }
                leftEye.transform.rotation = Quaternion.Slerp(leftEye.transform.rotation, leftEyeRotation, eyesRotationInertia * game.DeltaTimeRun);
                rightEye.transform.rotation = Quaternion.Slerp(rightEye.transform.rotation, rightEyeRotation, eyesRotationInertia * game.DeltaTimeRun);
            }
        }

        #endregion

        #region Shapeshift Methods

        public void ShapeshiftTo(ShapeshiftableObject _shapeshiftableObject)
        {
            // Buffer rotation
            Quaternion rotationBuffer = currentShapeshiftableObject.transform.localRotation;

            // Remove current object
            Destroy(currentShapeshiftableObject.gameObject);

            // Instantiate new object
            currentShapeshiftableObject = Instantiate(Resources.Load<GameObject>("Objects/" + _shapeshiftableObject.Resource)).GetComponent<ShapeshiftableObject>();
            currentShapeshiftableObject.transform.SetParent(transform, false);
            currentShapeshiftableObject.transform.localPosition = Vector3.zero;
            currentShapeshiftableObject.transform.localRotation = rotationBuffer;

            // Set look at
            cameraRotation.LookAt = currentShapeshiftableObject.LookAt;

            // Set rigidbody data
            movement.Rigidbody.mass = currentShapeshiftableObject.Mass;
            movement.Rigidbody.drag = currentShapeshiftableObject.Drag;
            movement.Rigidbody.angularDrag = currentShapeshiftableObject.AngularDrag;

            // Disable physics
            currentShapeshiftableObject.DisablePhysics();
        }

        public void Unshapeshift()
        {
            ShapeshiftTo(defaultShapeshiftableObject);
        }

        #endregion
    }
}