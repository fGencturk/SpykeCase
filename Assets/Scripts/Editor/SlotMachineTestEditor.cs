using System.Linq;
using Core.Data;
using Editor.Test;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SlotMachineTestEditor : EditorWindow
    {
        private SlotMachineConfig _slotMachineConfig;
        private int _testCount;

        [MenuItem("SPYKE/SlotMachineTestEditor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(SlotMachineTestEditor));
        }

        private void OnEnable()
        {
            var slotMachineConfigPaths = AssetDatabase.FindAssets($"t:{nameof(SlotMachineConfig)}");
            if (slotMachineConfigPaths.Length == 0)
            {
                Debug.LogError($"No {nameof(SlotMachineConfig)} found in project!");
                return;
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(slotMachineConfigPaths.First());
            _slotMachineConfig = AssetDatabase.LoadAssetAtPath<SlotMachineConfig>(assetPath);
        }

        private void OnGUI()
        {
            _testCount = EditorGUILayout.IntField("Test count", _testCount);
            if (GUILayout.Button("Start Test"))
            {
                var test = new SlotMachineTest(_slotMachineConfig);
                for (var i = 0; i < _testCount; i++)
                {
                    Debug.LogWarning($"--------- Test {i + 1} ---------");
                    test.Execute();
                    Debug.Log("Success");
                }
            }
        }
    }
}