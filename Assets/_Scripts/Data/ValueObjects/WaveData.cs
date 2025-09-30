using System;
using System.Collections.Generic;
using _Scripts.Controllers.Enemy;
using _Scripts.Enums;
using _Scripts.Keys;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Data.ValueObjects
{
    [Serializable]
    public struct WaveData
    {
        public bool isBossWave;
        public float spawnInterval;
        public List<EntryData> entries;
        
        public BossData bossData;
    }

    [Serializable]
    public struct BossData
    {
        public List<EntryData> bossEntries;
        public float spawnInterval;
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(WaveData))]
    public class WaveDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
    
            var isBossProp = property.FindPropertyRelative("isBossWave");
            var spawnIntervalProp = property.FindPropertyRelative("spawnInterval");
            var entriesProp = property.FindPropertyRelative("entries");
            var bossDataProp = property.FindPropertyRelative("bossData");
    
            position.height = EditorGUIUtility.singleLineHeight;
    
            EditorGUI.PropertyField(position, isBossProp);
            position.y += EditorGUIUtility.singleLineHeight + 2;
    
            EditorGUI.PropertyField(position, spawnIntervalProp);
            position.y += EditorGUIUtility.singleLineHeight + 2;
    
            EditorGUI.PropertyField(position, entriesProp, true);
            position.y += EditorGUI.GetPropertyHeight(entriesProp) + 2;
    
            if (isBossProp.boolValue)
            {
                EditorGUI.PropertyField(position, bossDataProp, true);
                position.y += EditorGUI.GetPropertyHeight(bossDataProp) + 2;
            }
    
            EditorGUI.EndProperty();
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var isBossProp = property.FindPropertyRelative("isBossWave");
            var spawnIntervalProp = property.FindPropertyRelative("spawnInterval");
            var entriesProp = property.FindPropertyRelative("entries");
            var bossDataProp = property.FindPropertyRelative("bossData");
    
            float height = EditorGUIUtility.singleLineHeight * 2;
            height += EditorGUI.GetPropertyHeight(entriesProp) + 2;
    
            if (isBossProp.boolValue)
                height += EditorGUI.GetPropertyHeight(bossDataProp) + 2;
    
            return height;
        }
    }
#endif
}