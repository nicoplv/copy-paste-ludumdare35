using UnityEngine;

namespace Cores
{
    public class TriggerUnlockPower : MonoBehaviour
    {
        #region Enums

        public enum Power
        {
            CopyPaste,
            Shapeshift,
        }

        #endregion

        #region Variables Methods

        protected Core core;

        [SerializeField]
        protected Power power;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            core = Core.Instance;
        }

        public void OnTriggerEnter(Collider _collider)
        {
            if (_collider.tag == Statics.Tags.Player)
            {
                switch (power)
                {
                    case Power.CopyPaste:
                        core.UnlockCopyPaste();
                        break;
                    case Power.Shapeshift:
                        core.UnlockShapeshift();
                        break;
                }
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}