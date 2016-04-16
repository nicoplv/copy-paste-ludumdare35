using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SceneExtensions
{
#if UNITY_EDITOR
    public static void AddToBuild(this Scene _scene)
    {
        // Create new list and copy old on new
        EditorBuildSettingsScene[] editorBuildSettingsScene = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + 1];
        System.Array.Copy(EditorBuildSettings.scenes, editorBuildSettingsScene, EditorBuildSettings.scenes.Length);

        // Add new scene to list on last position
        editorBuildSettingsScene[EditorBuildSettings.scenes.Length] = new EditorBuildSettingsScene(_scene.path, true);

        // Save the new list
        EditorBuildSettings.scenes = editorBuildSettingsScene;
    }

    public static void RemoveToBuild(this Scene _scene)
    {
        // Create new list
        EditorBuildSettingsScene[] editorBuildSettingsScene = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length -1];

        // Add scene to list
        int i = 0;
        bool find = false;
        foreach (EditorBuildSettingsScene iEditorBuildSettingsScene in EditorBuildSettings.scenes)
        {
            if (iEditorBuildSettingsScene.path == _scene.path)
                find = true;
            else if (i < editorBuildSettingsScene.Length)
            {
                editorBuildSettingsScene[i] = iEditorBuildSettingsScene;
                i++;
            }
        }
        
        // Save the new list only if find the element to remove
        if(find)
            EditorBuildSettings.scenes = editorBuildSettingsScene;
    }
#endif
}