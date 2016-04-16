#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DesignPatterns
{
    public class Inspector : Editor
	{
		#region Variables

		protected bool defaultInspector;

        private bool saveChangeOn = false;
        private List<Object> objectsToSave = new List<Object>();

        #endregion

        #region Unity Methods

		public override void OnInspectorGUI()
		{
            if (EditorApplication.isPlaying)
			{
				// Default inspector during play
				DrawDefaultInspector();
			}
			else
            {
                if (defaultInspector)
				{
					// Button to change to custom inspector
					if (GUILayout.Button("Custom Inspector"))
						defaultInspector = false;

					EditorGUILayout.Space();

					// Default inspector
					DrawDefaultInspector();
				}
				else
				{
                    // Button to change to default inspector
                    if (GUILayout.Button("Default Inspector"))
						defaultInspector = true;

					EditorGUILayout.Space();

					// Custom inspector
					if (serializedObject.isEditingMultipleObjects)
						InspectorEditingMultiple();
					else
						InspectorEditingOne();

                    // Save change on if necessary
                    if (saveChangeOn)
                    {
                        // Make all object dirty
                        foreach(Object iObject in objectsToSave)
                            EditorUtility.SetDirty(iObject);

                        // Make scene dirty
                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());

                        // Reset save change variables
                        saveChangeOn = false;
                        objectsToSave.Clear();
                    }
                }
			}
		}

        #endregion

        #region Methods

        public void SaveChangeOnTarget()
        {
            saveChangeOn = true;
            objectsToSave.AddIfNotContains(target);
        }

        public void SaveChangeOn(Object _object)
        {
            saveChangeOn = true;
            objectsToSave.AddIfNotContains(_object);
        }

        #endregion

        #region Inspector Methods

        protected virtual void InspectorEditingMultiple()
		{
			EditorGUILayout.HelpBox("Custom Inspector not support multi-object edition", MessageType.None);
		}

		protected virtual void InspectorEditingOne()
		{
			EditorGUILayout.HelpBox("Custom Inspector not support one-object edition", MessageType.None);
		}

        #endregion
    }
}

#endif