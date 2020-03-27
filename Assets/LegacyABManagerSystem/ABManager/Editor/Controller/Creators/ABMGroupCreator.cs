using System;
using UnityEngine;
using ABManagerEditor.Models;
using UnityEditor;
using ABManagerEditor.Consts;
using System.IO;

namespace ABManagerEditor.Controller.Creators
{
    internal class ABMGroupCreator : ABManagerAbstract
    {
        internal ABGroup Create()
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Settings is not found", ABNames.FileSettings);
            }
            if (!Directory.Exists(ABPaths.GroupsDirectoryPath))
            {
                AssetDatabase.CreateFolder(ABPaths.MainDirecctoryPath, ABNames.Groups);
            }
            ABGroup newGroup = ScriptableObject.CreateInstance<ABGroup>();
            AssetDatabase.CreateAsset(newGroup, Path.Combine(ABPaths.GroupsDirectoryPath, $"newGroup{Settings.Items.Count}.asset"));
            Settings.Items.Add(newGroup);
            return InitialSetup(newGroup);
        }
        internal void Delete(ABGroup group)
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Settings is not found", ABNames.FileSettings);
            }
            string assetPath = AssetDatabase.GetAssetPath(group);
            if (string.IsNullOrEmpty(assetPath))
            {
                throw new FileNotFoundException("Group is not found", group.name);
            }
            if (Settings.Items.Contains(group))
            {
                Settings.Items.Remove(group);
            }
            AssetDatabase.DeleteAsset(assetPath);           
        }
        protected internal ABGroup InitialSetup(ABGroup createdObj)
        {
            if (createdObj == null)
                throw new NullReferenceException("GroupAssetBundles is null");
            createdObj.Name = $"newGroup{Settings.Items.Count}";
            createdObj.Version = "1";
            createdObj.BuildPath = createdObj.LocalLoadPath = createdObj.RemoteLoadPath = createdObj.Name;

            return createdObj;
        }
    }
}

