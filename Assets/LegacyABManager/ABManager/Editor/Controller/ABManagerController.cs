using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Models;
using ABManagerEditor.Consts;


namespace ABManagerEditor.Controller
{
    internal class ABManagerController : ABManagerAbstract
    {
        internal ABManagerCreators Creators { get; }
        internal ABManagerBuilder Builder { get; }
        internal ABManagerController()
        {
            Creators = new ABManagerCreators();
            Builder = new ABManagerBuilder();
        }

        internal void SaveSettings()
        {
            if (Settings == null)
            {
                Debug.LogWarning("Сохранение не произошло");
                throw new NullReferenceException("Settings is null");
            }
            foreach (var group in Settings.Items)
            {
                SaveGroup(group);
            }
            EditorUtility.SetDirty(Settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        internal void SaveGroup(ABGroup group, bool isSaveAssets = false)
        {
            if (group == null)
            {
                Debug.LogWarning($"Сохранение группы {group.Name} не произошло, т.к он является null");
                return;
            }
            EditorUtility.SetDirty(group);
            if (isSaveAssets)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}

