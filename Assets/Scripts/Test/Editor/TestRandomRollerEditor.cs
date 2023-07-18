using UnityEditor;
using UnityEngine;

namespace Test.Editor
{
    [CustomEditor(typeof(TestRandomRoller))]
    public class TestRandomRollerEditor : UnityEditor.Editor
    {
        public override void  OnInspectorGUI ()
        {
            var testRandomRoller = (TestRandomRoller)target;
            if(GUILayout.Button("Roll from 0-99")) {
                testRandomRoller.RollUntil();
            }
            DrawDefaultInspector();
        }
    }
}