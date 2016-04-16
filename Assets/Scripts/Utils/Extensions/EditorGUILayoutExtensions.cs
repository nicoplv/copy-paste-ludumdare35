using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class EditorGUILayoutExtensions : MonoBehaviour
{
    static List<string> layers;
    static long lastUpdateTick;

    public static LayerMask LayerMaskField(string _label, LayerMask _selected)
    {
        if (layers == null || (System.DateTime.UtcNow.Ticks - lastUpdateTick > 10000000L && Event.current.type == EventType.Layout))
        {
            lastUpdateTick = System.DateTime.UtcNow.Ticks;
            layers = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                if (LayerMask.LayerToName(i) != "")
                    layers.Add(LayerMask.LayerToName(i));
                else
                    layers.Add("-- Layer " + i + " --");
            }
        }

        return EditorGUILayout.MaskField(_label, _selected.value, layers.ToArray());
    }
}