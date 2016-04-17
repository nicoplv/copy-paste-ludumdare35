using UnityEngine;

namespace Players
{
    public class TriggerText : MonoBehaviour
    {
        #region Variables Methods

        protected Player player;

        [SerializeField]
        protected string text = "";

        #endregion

        #region Unity Methods

        public void Awake()
        {
            player = Player.Instance;
        }

        public void OnTriggerStay(Collider _collider)
        {
            if (_collider.tag == Statics.Tags.Player)
            {
                if(!player.CurrentShapeshiftableObject.Bulle.IsActive)
                    player.CurrentShapeshiftableObject.Bulle.SetText(text);
            }
        }

        public void OnTriggerExit(Collider _collider)
        {
            if (_collider.tag == Statics.Tags.Player)
                player.CurrentShapeshiftableObject.Bulle.UnsetText();
        }

        #endregion
    }
}