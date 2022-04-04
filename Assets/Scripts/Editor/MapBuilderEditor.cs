using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MapBuilder))]
public class MapBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapBuilder builder = (MapBuilder)target;
        if (DrawDefaultInspector())
        {
            if (builder.autoUpdate)
            {
                builder.GenerateMap();
            }
        };
        if (GUILayout.Button("Generate")) builder.GenerateMap();
    }
}
