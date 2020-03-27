using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using ABManagerEditor.Controller;
using ABManagerEditor.Models;
using ABManagerEditor.Interfaces;

namespace ABManagerEditor.Browser.Blocks
{
    internal class ABGroupTree : TreeView, ISelectABGroup
    {
        public event Action<ABGroup> OnSelectABGroup;

        private readonly ABManagerController _controller;
        private ABGroupTreeViewItem _parentToAllGroups;
        private bool _contextOnItem;
        internal ABGroupTree(TreeViewState state, ABManagerController controller) : base(state)
        {
            _controller = controller;
            Reload();
        }
        protected override TreeViewItem BuildRoot()
        {
            var root = CreateGroupTreeView();
            return root;
        }
        private ABGroupTreeViewItem CreateGroupTreeView()
        {
            var root = new ABGroupTreeViewItem(0, -1);
            if (_controller.Settings.Items.Count == 0)
            {
                var simpleChildren = new TreeViewItem(1, 0);
                root.AddChild(simpleChildren);
                _parentToAllGroups = null;
            }
            else
            {
                _parentToAllGroups = root;
                var groups = _controller.Settings.Items;
                foreach (var group in groups)
                {
                    if (group != null)
                    {
                        var newItem = new ABGroupTreeViewItem(group, 1);
                        _parentToAllGroups.AddChild(newItem);
                    }
                }
            }
            SetupDepthsFromParentsAndChildren(root);
            return root;
        }
        public override void OnGUI(Rect screenRect)
        {
            float treeHeight = screenRect.height - 20;
            var treeRect = new Rect(screenRect.x, screenRect.y, screenRect.width, screenRect.height - 20);
            base.OnGUI(screenRect);
            GUILayout.Space(treeHeight);
            if (GUILayout.Button("Add New Group", GUILayout.Height(20)))
            {
                CreateNewGroup();
            }
            
        }
        protected override void SelectionChanged(IList<int> selectedIds)
        {
            var selectedItems = new List<ABGroupTreeViewItem>();
            if (selectedIds != null && selectedIds.Count == 1)
            {
                foreach (var id in selectedIds)
                {
                    var item = FindItem(id, rootItem) as ABGroupTreeViewItem;
                    if (item != null && item.Group != null)
                    {
                        OnSelectABGroup?.Invoke(item.Group);
                    }
                }
            }
            else
            {
                Debug.Log("Чтобы посмотреть настройки и ассеты, выберите только одну группу");
            }
        }
        protected override bool CanRename(TreeViewItem item)
        {
            return true;
        }
        protected override void RenameEnded(RenameEndedArgs args)
        {
            if (args.acceptedRename)
            {
                var remanedItem = FindItem(args.itemID, _parentToAllGroups) as ABGroupTreeViewItem;
                if (string.IsNullOrEmpty(args.newName))
                {
                    remanedItem.Group.Name = args.originalName;
                }
                else
                {
                    remanedItem.Group.Name = args.newName;
                }
            }
        }
        protected override void ContextClicked()
        {
            if (_contextOnItem)
            {
                _contextOnItem = false;
                return;
            }
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Добавить новую группу"), false, CreateNewGroup);

            menu.ShowAsContext();
        }
        protected override void ContextClickedItem(int id)
        {
            _contextOnItem = true;

            List<ABGroupTreeViewItem> selectedItems = new List<ABGroupTreeViewItem>();
            foreach (var nodeID in GetSelection())
            {
                selectedItems.Add(FindItem(nodeID, rootItem) as ABGroupTreeViewItem);
            }        
                GenericMenu menu = new GenericMenu();
            if (selectedItems.Count == 1)
            {
                menu.AddItem(new GUIContent("Удалить " + selectedItems[0].displayName), false, DeleteGroups, selectedItems);
            }
            else if (selectedItems.Count > 1)
            {
                menu.AddItem(new GUIContent("Удалить " + selectedItems.Count + " выбранные группы"), false, DeleteGroups, selectedItems);
            }
            menu.ShowAsContext();
        }
        private void DeleteGroups(object groups)
        {
            var selectedItems = groups as List<ABGroupTreeViewItem>;
            foreach (var item in selectedItems)
            {
                _controller.Creators.GroupCreator.Delete(item.Group);
            }
            Refresh();
        }
        private void CreateNewGroup()
        {
            _controller.Creators.GroupCreator.Create();
            Refresh();
        }
        internal void Refresh()
        {
            Reload();
            SetFocusAndEnsureSelectedItem();
        }
    }
}

