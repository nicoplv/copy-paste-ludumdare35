using UnityEngine;

namespace Players
{
    public class Gaze : MonoBehaviour
    {
        #region Variables

        protected Games.Game game;
        protected Controlers.Controler controler;

        [SerializeField]
        protected Player player;
        public Player Player { get { return player; } }

        [SerializeField]
        protected float maxDistance = 10f;

        protected ShapeshiftableObjects.ShapeshiftableObject shapeshiftableObjectInView = null;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            game = Games.Game.Instance;
            controler = Controlers.Controler.Instance;
        }

        public void Update()
        {
            if (game.IsRun)
            {
                // Object in view
                RaycastHit raycastHit;
                if (Physics.Raycast(player.CameraRotation.Camera.transform.position, player.CameraRotation.Camera.transform.forward, out raycastHit, maxDistance, Statics.Layers.Object.Mask))
                {
                    ShapeshiftableObjects.ShapeshiftableObject shapeshiftableObjectInViewBuffer = shapeshiftableObjectInView;
                    shapeshiftableObjectInView = raycastHit.collider.GetComponent<ShapeshiftableObjects.ShapeshiftableObject>();
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
                    if(shapeshiftableObjectInView != null)
                        shapeshiftableObjectInView.InViewEnd();
                    shapeshiftableObjectInView = null;
                }

                // Player Shapeshift
                if (controler.PlayerShapeshift() && shapeshiftableObjectInView != null)
                    player.ShapeshiftTo(shapeshiftableObjectInView);

                // Player Unshapeshift
                if (controler.PlayerUnshapeshift())
                    player.Unshapeshift();
            }
        }

        #endregion
    }
}