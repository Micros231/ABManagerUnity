using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using ABManagerEditor.Controller.Creators;
using ABManagerEditor.Models;
using UnityEditor.VersionControl;

namespace ABManagerEditor.Browser.Blocks
{
    internal class AssetsBlock : AbstractBlock
    {
        [SerializeField] 
        TreeViewState _treeViewState;

        private ABAssetTree _assetTreeView;
        private ABGroup _currentGroup;
        private ABAsset _currentAsset;
        private Object _assetObject;
        private readonly ABMAssetCreator _creator;
        internal AssetsBlock(ABMAssetCreator creator)
        {
            _creator = creator;
        }
        internal override void OnEnable()
        {
            if (_treeViewState == null)
                _treeViewState = new TreeViewState();

            _assetTreeView = new ABAssetTree(_treeViewState, _creator);
            _assetTreeView.OnSelectABAsset += OnSelectAsset;
        }
        internal override void OnDisable()
        {
            _assetTreeView.OnSelectABAsset -= OnSelectAsset;
        }
        internal override void OnGUI(Rect screenRect)           
        {
            float paddingHeight = 30;
            GUILayout.Label("Ассеты:",
                new GUIStyle { alignment = TextAnchor.MiddleCenter },
                GUILayout.Height(paddingHeight),
                GUILayout.Width(screenRect.width));
            float treePositionY = screenRect.y + paddingHeight;
            float treeHeight = (screenRect.height - treePositionY) / 1.8f;
            var treeRect = new Rect(
                screenRect.x,
                treePositionY,
                screenRect.width,
                treeHeight);
            if (_currentGroup == null)
            {
                EditorGUILayout.HelpBox("Группа не выбрана", MessageType.Info);
            }
            else
            {
                _assetTreeView.OnGUI(treeRect);
                GUILayout.Space(treeHeight);
                GUILayout.BeginHorizontal();
                _assetObject = EditorGUILayout.ObjectField(_assetObject, typeof(Object), false);
                if (_assetObject != null)
                {
                    if (GUILayout.Button("Add"))
                    {
                        _assetTreeView.AddAsset(_assetObject);
                        _assetObject = null;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.Label("Настройка Ассета:",
                new GUIStyle { alignment = TextAnchor.MiddleCenter },
                GUILayout.Height(paddingHeight),
                GUILayout.Width(screenRect.width));
                if (_currentGroup.Items.FirstOrDefault(asset => asset == _currentAsset) == null)
                {
                    EditorGUILayout.HelpBox("Ассет не выбран", MessageType.Info);
                }
                else
                {
                    _currentAsset.Name = EditorGUILayout.TextField("Имя ассета:", _currentAsset.Name);
                    EditorGUILayout.LabelField("Путь:", _currentAsset.PathAsset);
                    EditorGUILayout.LabelField("Тип:", _currentAsset.TypeAsset.ToString());
                    EditorGUILayout.LabelField("GUID:", _currentAsset.GUIDAsset.ToString());
                }
            }
        }
        internal void UpdateGroup(ABGroup group)
        {
            if (group != null)
            {
                _currentGroup = group;
                _assetTreeView.SetFocusAndEnsureSelectedItem();
                _assetTreeView.UpdateGroup(group);
                _currentAsset = null;
            }
        }
        private void OnSelectAsset(ABAsset asset)
        {
            _currentAsset = asset;
        }
    }
}

