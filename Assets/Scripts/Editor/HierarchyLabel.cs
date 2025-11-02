using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyLabel : MonoBehaviour
{
    static HierarchyLabel()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj != null && obj.name.StartsWith("---", System.StringComparison.Ordinal))
        {
            EditorGUI.DrawRect(selectionRect, Color.grey);
            EditorGUI.DropShadowLabel(selectionRect, obj.name.Replace("-", "").ToString());
        }

        HighlightObj(obj, "FLDR_ROCK", selectionRect, Color.red);
        HighlightObj(obj, "DamageDetector", selectionRect, Color.green);
        HighlightObj(obj, "SceneRoot", selectionRect, Color.blue);
    }

    static void HighlightObj(GameObject obj, string objname, Rect selectionRect, Color color)
    {
        if (obj != null && obj.name.Equals(objname))
        {
            EditorGUI.DrawRect(selectionRect, color);
            EditorGUI.DropShadowLabel(selectionRect, obj.name);
        }
    }
}
