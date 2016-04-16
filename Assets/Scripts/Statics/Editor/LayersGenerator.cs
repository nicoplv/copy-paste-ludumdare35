using UnityEditor;
using UnityEngine;
using System.IO;

public class LayersGenerator : MonoBehaviour
{
    #region Variables

    protected static string scriptFolderPathAbsolute = Application.dataPath + "/Scripts/Statics/";

    #endregion

    #region Menu Item

    [MenuItem("GameSimply/Generators/Layers")]
    public static void MenuItem()
    {
        // Create all folders if necessary
        if (!Directory.Exists(scriptFolderPathAbsolute))
            Directory.CreateDirectory(scriptFolderPathAbsolute);

        // Create the file
        File.WriteAllText(scriptFolderPathAbsolute + "Layers.cs", CreateLayers());

        // Refresh assets
        AssetDatabase.Refresh();
    }

    #endregion

    #region Layers Generator Methods

    private static string CreateLayers()
    {
        string output = "";

        output += "// This class is auto-generated DO NOT MODIFY\n";
        output += "// Use GameSimply >> Generators >> Layers on the menu to update this class\n";
        output += "\n";
        output += "using System.Collections.Generic;\n";
        output += "using Utils;\n";
        output += "\n";
        output += "namespace Statics\n";
        output += "{\n";
        output += "\tpublic static class Layers\n";
        output += "\t{\n";
        output += "\t\t#region Variables\n";
        output += "\t\t\n";

        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            output += "\t\tpublic static Layer " + UnityEditorInternal.InternalEditorUtility.layers[i].Clean() + " = new Layer(\"" + UnityEditorInternal.InternalEditorUtility.layers[i] + "\");\n";
        }

        output += "\t\t\n";
        output += "\t\t#endregion\n";
        output += "\t\n";
        output += "\t\t#region List\n";
        output += "\t\t\n";
        output += "\t\tpublic static List<Layer> List = new List<Layer>()\n";
        output += "\t\t{\n";

        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            output += "\t\t\t" + UnityEditorInternal.InternalEditorUtility.layers[i].Clean() + ",\n";
        }

        output += "\t\t};\n";
        output += "\t\t\n";
        output += "\t\t#endregion\n";
        output += "\t}\n";
        output += "}";

        return output;
    }

    #endregion
}