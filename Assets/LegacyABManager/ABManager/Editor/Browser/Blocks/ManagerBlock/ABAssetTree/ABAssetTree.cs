using ABManagerEditor.Controller.Creators;
using ABManagerEditor.Models;
using ABManagerEditor.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEditor;

namespace ABManagerEditor.Browser.Blocks
{
    internal class ABAssetTree : TreeView, ISelectABAsset
    {
        private readonly ABMAssetCreator _creator;
        private ABAssetTreeItem _parentToAllAssets;
        private ABGroup _currentGroup;
        private bool _contextOnItem;

        public event Action<ABAsset> OnSelectABAsset;

        internal ABAssetTree(TreeViewState state, ABMAssetCreator creator) : base(state)
        {
            _creator = creator;
            Refresh();
        }
        protected override TreeViewItem BuildRoot()
        {
            var root = CreateGroupTreeView();
            return root;
        }
        protected override void SelectionChanged(IList<int> selectedIds)
        {
            var selectedItems = new List<ABAssetTreeItem>();
            if (selectedIds != null && selectedIds.Count == 1)
            {
                foreach (var id in selectedIds)
                {
                    var item = FindItem(id, rootItem) as ABAssetTreeItem;
                    if (item != null && item.Asset != null)
                    {
                        OnSelectABAsset?.Invoke(item.Asset);
                    }
                }
            }
            else
            {
                Debug.Log("Чтобы посмотреть настройки и ассеты, выберите только одну группу");
            }
        }
        protected override void ContextClickedItem(int id)
        {
            _contextOnItem = true;

            List<ABAssetTreeItem> selectedItems = new List<ABAssetTreeItem>();
            foreach (var nodeID in GetSelection())
            {
                selectedItems.Add(FindItem(nodeID, rootItem) as ABAssetTreeItem);
            }

            GenericMenu menu = new GenericMenu();
            if (selectedItems.Count == 1)
            {
                menu.AddItem(new GUIContent($"Удалить \"{selectedItems[0].Asset.GUIDAsset}\""), false, DeleteAssets, selectedItems);
            }
            else if (selectedItems.Count > 1)
            {
                menu.AddItem(new GUIContent("Удалить " + selectedItems.Count + " выбранные ассеты"), false, DeleteAssets, selectedItems);
            }
            menu.ShowAsContext();
        }
        protected override bool CanStartDrag(CanStartDragArgs args)
        {
            args.draggedItemIDs = GetSelection();
            return true;
        }
        protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
        {
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.objectReferences = _currentGroup.Items.Select(asset => asset.AssetObject).ToArray();
            List<ABAssetTreeItem> items = new List<ABAssetTreeItem>(args.draggedItemIDs.Select(id => FindItem(id, rootItem) as ABAssetTreeItem));
            DragAndDrop.paths = items.Select(item => item.Asset.PathAsset).ToArray();
            DragAndDrop.SetGenericData("ABAssetListTreeSource", this);
            DragAndDrop.StartDrag("ABAssetListTree");
        }
        protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
        {
            if (IsValidDragAndDrop())
            {
                if (args.performDrop)
                {
                    foreach (var obj in DragAndDrop.objectReferences)
                    {
                        AddAsset(obj);
                    }
                    
                }
                return DragAndDropVisualMode.Copy;
            }
            return DragAndDropVisualMode.Rejected;
        }
        protected override void DoubleClickedItem(int id)
        {
            var item = FindItem(id, rootItem) as ABAssetTreeItem;
            if (item != null)
            {
                EditorGUIUtility.PingObject(item.Asset.AssetObject);
            }
        }
        internal void AddAsset(UnityEngine.Object assetObject)
        {
            if (_currentGroup != null)
            {
                _creator.CreateAndAddToCollection(assetObject, _currentGroup);
            }
            Refresh();
        }
        internal void UpdateGroup(ABGroup group)
        {
            if (group != null)
            {
                _currentGroup = group;
                Refresh();
            }
        }
        internal void Refresh()
        {
            Reload();
        }

        private bool IsValidDragAndDrop()
        {
            if (DragAndDrop.paths == null || DragAndDrop.paths.Length == 0)
                return false;
            var thing = DragAndDrop.GetGenericData("ABAssetListTreeSource") as ABAssetTree;
            if (thing != null)
                return false;
            if (_currentGroup == null)
                return false;
            return true;
        }
        private void DeleteAssets(object assets)
        {
            var selectedItems = assets as List<ABAssetTreeItem>;
            foreach (var item in selectedItems)
            {
                _creator.Delete(item.Asset, _currentGroup);
            }
            Refresh();
        }
        private ABAssetTreeItem CreateGroupTreeView()
        {
            var root = new ABAssetTreeItem(0, -1);
            if (_currentGroup == null || _currentGroup.Items.Count == 0)
            {
                var simpleChildren = new TreeViewItem(1, 0);
                root.AddChild(simpleChildren);
                _parentToAllAssets = null;
            }
            else
            {
                _parentToAllAssets = root;
                var assets = _currentGroup.Items;
                foreach (var asset in assets)
                {
                    if (asset != null)
                    {
                        var newItem = new ABAssetTreeItem(asset, 1);
                        _parentToAllAssets.AddChild(newItem);
                    }
                }
            }
            SetupDepthsFromParentsAndChildren(root);
            return root;
        }
    }
}

