using UnityEngine;

namespace Players
{
    public class Bulle : MonoBehaviour
    {
        #region Variables

        protected Player player;

        [SerializeField]
        protected GameObject canvas;

        [SerializeField]
        protected UnityEngine.UI.Text text;

        [SerializeField]
        protected float rotationCorrection = 0f;

        protected bool isActive = false;
        public bool IsActive { get { return isActive; } }

        #endregion

        #region Unity

        public void Awake()
        {
            player = Player.Instance;

            // Disable by default
            UnsetText();
        }

        public void Update()
        {
            transform.LookAt(player.CameraRotation.Camera.transform.position + Vector3.down * rotationCorrection);
        }

        #endregion

        #region Methods

        public void SetText(string _text)
        {
            canvas.SetActive(true);
            text.text = _text;
            isActive = true;
        }

        public void UnsetText()
        {
            canvas.SetActive(false);
            text.text = "";
            isActive = false;
        }

        #endregion
    }
}