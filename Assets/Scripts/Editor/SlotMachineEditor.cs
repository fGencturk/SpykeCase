using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Infrastructure.Save;
using Random.Milestone;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    class SlotMachineEditor : EditorWindow 
    {
        private SlotMachineConfig _slotMachineConfig;
        private CombinationMilestoneProvider _milestoneProvider;
        private bool _intervalsVisible;
        private bool _saveDataVisible;
        private List<int> _editingSaveData;
        private Vector2 _scrollPosition;

        [MenuItem ("SPYKE/SlotMachineEditor")]
        public static void  ShowWindow () {
            EditorWindow.GetWindow(typeof(SlotMachineEditor));
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
            _milestoneProvider = new CombinationMilestoneProvider(_slotMachineConfig);

            _editingSaveData = new List<int>();
            var playerData = new SlotMachinePlayerData();
            for (var i = 0; i < _slotMachineConfig.SlotCombinationConfigs.Count; i++)
            {
                _editingSaveData.Add(playerData.GetLastHitIndex(i));
            }
        }

        void OnGUI ()
        {
            if (_slotMachineConfig == null)
            {
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            _intervalsVisible = EditorGUILayout.BeginFoldoutHeaderGroup(_intervalsVisible, "Combination Intervals");

            if (_intervalsVisible)
            {
                for(var combinationIndex = 0; combinationIndex < _slotMachineConfig.SlotCombinationConfigs.Count; combinationIndex++)
                {
                    var slotCombinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                    EditorGUILayout.LabelField($"Combination Index: {combinationIndex}, [{string.Join(",", slotCombinationConfig.SlotItems)}] with Percentage: {slotCombinationConfig.Percentage}");
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    for (var i = 0; i < slotCombinationConfig.Percentage; i++)
                    {
                        var interval = _milestoneProvider.GetCombinationMilestoneOfIntervalIndex(combinationIndex, i);
                        EditorGUILayout.LabelField($"[{interval.Min}, {interval.Max}]", GUILayout.Width(50));
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(10);
                }
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            _saveDataVisible = EditorGUILayout.BeginFoldoutHeaderGroup(_saveDataVisible, "Edit save data");
            

            if (_saveDataVisible)
            {
                EditorGUIUtility.labelWidth = 250f;
                for(var combinationIndex = 0; combinationIndex < _slotMachineConfig.SlotCombinationConfigs.Count; combinationIndex++)
                {
                    var slotCombinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                    EditorGUILayout.BeginVertical();
                    
                    EditorGUILayout.LabelField($"Combination Index: {combinationIndex}, [{string.Join(",", slotCombinationConfig.SlotItems)}] with Percentage: {slotCombinationConfig.Percentage}");
                    var intervalText = "NEVER";
                    if (_editingSaveData[combinationIndex] >= 0)
                    {
                        var rolledInterval =
                            _milestoneProvider.GetCombinationMilestoneOfRollIndex(combinationIndex,
                                _editingSaveData[combinationIndex]);
                        intervalText = $"[{rolledInterval.Min},{rolledInterval.Max}]";
                    }
                    _editingSaveData[combinationIndex] = EditorGUILayout.IntField($"Last rolled index in range {intervalText} : ", _editingSaveData[combinationIndex]);

                    EditorGUILayout.EndVertical();
                    
                    GUILayout.Space(10);
                }
                var currentRollIndex = _editingSaveData.Max();
                EditorGUILayout.LabelField($"Current roll index : {currentRollIndex}");
                
                if (GUILayout.Button("Save all"))
                {
                    var playerData = new SlotMachinePlayerData();
                    for (var i = 0; i < _editingSaveData.Count; i++)
                    {
                        playerData.SetLastHitIndex(i, _editingSaveData[i]);
                        playerData.CurrentRollIndex = _editingSaveData.Max();
                    }
                }
                
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndScrollView();
        }
    }
}