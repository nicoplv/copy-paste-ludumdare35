using UnityEngine;
using System.Collections.Generic;

namespace Players
{
    public class TriggerUnactive : MonoBehaviour
    {
        #region Variables Methods

        [SerializeField]
        protected List<GameObject> gameObjects = new List<GameObject>();

        #endregion

        #region Unity Methods

        public void OnTriggerEnter(Collider _collider)
        {
            foreach (GameObject iGameObject in gameObjects)
                iGameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        #endregion
    }
}