using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class TriggerEnd : MonoBehaviour
    {
        #region Variables Methods

        [SerializeField]
        protected GameObject gameObjectEnd;

        #endregion

        #region Unity Methods

        public void OnTriggerEnter(Collider _collider)
        {
            if (_collider.tag == Statics.Tags.Player)
            {
                gameObjectEnd.SetActive(true);
            }
        }

        #endregion
    }
}