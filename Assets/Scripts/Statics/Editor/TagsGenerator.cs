using UnityEditor;
using UnityEngine;
using System.IO;

public class TagsGenerator : MonoBehaviour
{
    #region Variables
    
    protected static string scriptFolderPathAbsolute = Application.dataPath + "/Scripts/Statics/";

    #endregion

    #region Menu Item

    [MenuItem("GameSimply/Generators/Tags")]
    public static void MenuItem()
    {
        // Create all folders if necessary
        if (!Directory.Exists(scriptFolderPathAbsolute))
            Directory.CreateDirectory(scriptFolderPathAbsolute);

        // Create the file
        File.WriteAllText(scriptFolderPathAbsolute + "Tags.cs", CreateTags());

        // Refresh assets
        AssetDatabase.Refresh();
    }

    #endregion

    #region Tags Generator Methods

    private static string CreateTags()
    {
        string output = "";

        output += "// This class is auto-generated DO NOT MODIFY\n";
        output += "// Use GameSimply >> Generators >> Tags on the menu to update this class\n";
        output += "\n";
        output += "using System.Collections.Generic;\n";
        output += "\n";
        output += "namespace Statics\n";
        output += "{\n";
        output += "\tpublic static class Tags\n";
        output += "\t{\n";
        output += "\t\t#region Variables\n";
        output += "\t\t\n";

        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            output += "\t\tpublic const string " + UnityEditorInternal.InternalEditorUtility.tags[i].Clean() + " = \"" + UnityEditorInternal.InternalEditorUtility.tags[i] + "\";\n";
        }

        output += "\t\t\n";
        output += "\t\t#endregion\n";
        output += "\t\n";
        output += "\t\t#region List\n";
        output += "\t\t\n";
        output += "\t\tpublic static List<string> List = new List<string>()\n";
        output += "\t\t{\n";

        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            output += "\t\t\t" + UnityEditorInternal.InternalEditorUtility.tags[i].Clean() + ",\n";
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