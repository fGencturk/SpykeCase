using UnityEditor;
using UnityEngine;

namespace Test.Editor
{
    [CustomEditor(typeof(TestRandomRoller))]
    public class TestRandomRollerEditor : UnityEditor.Editor
    {

        private int _rollIndex = 100;
        
        public override void  OnInspectorGUI ()
        {

            _rollIndex = EditorGUILayout.IntField("Roll until index", _rollIndex);
            _rollIndex = Mathf.Clamp(_rollIndex, 0, 100);
            
            var testRandomRoller = (TestRandomRoller)target;
            if(GUILayout.Button("Roll until index")) {
                testRandomRoller.RollUntil(_rollIndex);
            }
            DrawDefaultInspector();
        }
    }
}