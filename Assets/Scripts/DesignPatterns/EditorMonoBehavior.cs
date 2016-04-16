using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DesignPatterns
{
	[ExecuteInEditMode]
	public class EditorMonoBehavior : MonoBehaviour
	{
		#region Variables

		protected bool unactiveGameObject = false;

        #endregion

        #region Unity Methods

        public void Awake()
        {
            // Check if in editor mode
            bool editorMode = false;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                editorMode = true;
#endif

            // Disable if not on editor mode
            if (!editorMode)
            {
                if (!unactiveGameObject)
                    enabled = false;
                else
                    gameObject.SetActive(false);
            }
        }

        #endregion
    }
}