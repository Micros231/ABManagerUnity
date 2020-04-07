using System;
using System.Linq;
using ABManagerEditor.Consts;
using ABManagerEditor.Models;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Controller.Creators
{
    internal class ABMAssetCreator : ABManagerAbstract
    {
        internal ABAsset CreateAndAddToCollection(UnityEngine.Object assetObject, ABGroup group)
        {
            
            if (IsCreateAndAddToCollectionValid(assetObject, group))
            {
                ABAsset asset = new ABAsset(assetObject);
                group.Items.Add(asset);
                SaveGroup(group);
                return asset;
            }
            throw new Exception("Ассет не создался из-за ошибок");
        }
        internal void Delete(ABAsset asset, ABGroup group)
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Settings is not found", ABNames.FileSettings);
            }
            if (asset == null)
            {
                throw new ArgumentNullException("SimpleAsset", "SimpleAsset is null");
            }
            if (group == null)
            {
                throw new ArgumentNullException("GroupAssetBundlesParent", "GroupAssetBundlesParent is null");
            }
            group.Items.Remove(asset);
            SaveGroup(group);
        }
        private void SaveGroup(ABGroup group)
        {
            EditorUtility.SetDirty(group);
            AssetDatabase.SaveAssets();
        }

        private bool IsCreateAndAddToCollectionValid(UnityEngine.Object assetObject, ABGroup group)
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Settings is not found", ABNames.FileSettings);
            }
            if (assetObject == null)
            {
                throw new ArgumentNullException("AssetObject", "AssetObject is null");
            }
            if (group == null)
            {
                throw new ArgumentNullException("GroupAssetBundlesParent", "GroupAssetBundlesParent is null");
            }
            if (group.Items.Select(_asset => _asset.AssetObject).FirstOrDefault(obj => obj == assetObject) != null)
            {
                EditorGUIUtility.PingObject(assetObject);
                throw new ArgumentException("В этой группе уже есть этот ассет");
            }
                return true;
            //throw new ArgumentException("Ассет не соответствует нужному типу");
        }
    }
}

