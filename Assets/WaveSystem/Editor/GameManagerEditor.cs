using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//EDITOR TUTORIAL https://www.youtube.com/watch?v=Pba1x7D8pMQ

[CustomEditor(typeof(GameManager)), CanEditMultipleObjects]
public class GameManagerEditor : Editor
{
    private SerializedProperty
        waveCreator_prop;

    private ReorderableList waveList;
    private ReorderableList waveType;

    private void OnEnable()
    {
        waveCreator_prop = serializedObject.FindProperty("waveCreator");

        waveList = new ReorderableList(serializedObject, waveCreator_prop, true, true, true, true);

        waveList.drawElementCallback = DrawlistItems;
        waveList.drawHeaderCallback = DrawHeader;
    }

    void DrawlistItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = waveList.serializedProperty.GetArrayElementAtIndex(index);
        waveType = new ReorderableList(serializedObject, element.FindPropertyRelative("pool"), true, true, true, true);

        waveType.drawHeaderCallback = DrawSubHeader;

        //EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
        //    element.FindPropertyRelative("index"),
        //    GUIContent.none
        //    );
    }

    void DrawHeader(Rect rect)
    {
        string name = "Wave";
        EditorGUI.LabelField(rect, name);
    }

    void DrawSubHeader(Rect rect)
    {
        string name = "subWave";
        EditorGUI.LabelField (rect, name);
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        waveList.DoLayoutList();
        waveType.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
