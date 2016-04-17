using UnityEngine;

namespace Players
{
    public class Gaze : MonoBehaviour
    {
        #region Variables

        protected Games.Game game;
        protected Cores.Core core;
        protected Controlers.Controler controler;

        [SerializeField]
        protected Player player;
        public Player Player { get { return player; } }

        [SerializeField]
        protected float upCorrection = .25f;

        [SerializeField]
        protected float maxDistance = 10f;

        protected ShapeshiftableObject shapeshiftableObjectInView = null;

        protected ShapeshiftableObject shapeshiftableObjectCopied = null;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            game = Games.Game.Instance;
            core = Cores.Core.Instance;
            controler = Controlers.Controler.Instance;
        }

        public void Update()
        {
            if (game.IsRun)
            {
                // Object in view
                if (core.PowerShapeshift || core.PowerCopyPaste)
                {
                    RaycastHit raycastHit;
                    if (Physics.Raycast(player.CameraRotation.LookAt.transform.position, player.CameraRotation.Camera.transform.forward + (upCorrection * Vector3.up), out raycastHit, maxDistance, Statics.Layers.Object.Mask | Statics.Layers.Collider.Mask))
                    {
                        ShapeshiftableObject shapeshiftableObjectInViewBuffer = shapeshiftableObjectInView;
                        shapeshiftableObjectInView = raycastHit.collider.GetComponent<ShapeshiftableObject>();
                        if (shapeshiftableObjectInView != shapeshiftableObjectInViewBuffer)
                        {
                            if (shapeshiftableObjectInView != null)
                                shapeshiftableObjectInView.InViewStart();
                            if (shapeshiftableObjectInViewBuffer != null)
                                shapeshiftableObjectInViewBuffer.InViewEnd();
                        }
                    }
                    else
                    {
                        if (shapeshiftableObjectInView != null)
                            shapeshiftableObjectInView.InViewEnd();
                        shapeshiftableObjectInView = null;
                    }
                }

                // Player Shapeshift
                if (core.PowerShapeshift && controler.PlayerShapeshift() && shapeshiftableObjectInView != null)
                    player.ShapeshiftTo(shapeshiftableObjectInView);

                // Player Unshapeshift
                if (core.PowerShapeshift && controler.PlayerUnshapeshift())
                    player.Unshapeshift();

                // Player Copy
                if (core.PowerCopyPaste && controler.PlayerCopy() && shapeshiftableObjectInView != null)
                    shapeshiftableObjectCopied = shapeshiftableObjectInView;

                // Player Paste
                if (core.PowerCopyPaste && controler.PlayerPaste() && shapeshiftableObjectInView != null && shapeshiftableObjectCopied != null)
                {
                    // Instantiate new object
                    ShapeshiftableObject shapeshiftableObjectBuffer = Instantiate(Resources.Load<GameObject>("Objects/" + shapeshiftableObjectCopied.Resource)).GetComponent<ShapeshiftableObject>();
                    shapeshiftableObjectBuffer.transform.position = shapeshiftableObjectInView.transform.position;
                    shapeshiftableObjectBuffer.transform.rotation = shapeshiftableObjectInView.transform.rotation;

                    // Destroy old object in View
                    Destroy(shapeshiftableObjectInView.gameObject);
                }
            }
        }

        #endregion
    }
}