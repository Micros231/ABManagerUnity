using ABManagerEditor.BuildModels;
using ABManagerEditor.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks.Manager.LevelTemplate
{
    internal class LevelTemplatesTreeView : AbstractTreeView<ABLevelTemplate>
    {
        private TreeViewItem _hiddenChildren;
        public LevelTemplatesTreeView(TreeViewState state) : base(state)
        {
        }
        public override void SelectItem(ABLevelTemplate item)
        {
            
        }
        protected override TreeViewItem CreateGroupTreeView()
        {
            var root = new TreeViewItem(0, -1);
            if (ABController.Current.ManagerSettings.LevelTemplates.Count == 0)
            {
                _hiddenChildren = new TreeViewItem(1, 0);
                root.AddChild(_hiddenChildren);
            }
            else
            {
                var items = ABController.Current.ManagerSettings.LevelTemplates;
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        var newItem = new TemplateTreeViewItem<ABLevelTemplate>(item);
                        root.AddChild(newItem);
                    }
                }
                _hiddenChildren = null;
            }
            SetupDepthsFromParentsAndChildren(root);
            return root;
        }
        protected override void SelectionChanged(IList<int> selectedIds)
        {
            //base.SelectionChanged(selectedIds);
        }
        protected override void ContextClicked()
        {
            if (ContextOnItem)
            {
                ContextOnItem = false;
                return;
            }
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Добавить новый темплейт"), false, CreateNewItem);

            menu.ShowAsContext();
        }
        protected override void ContextClickedItem(int id)
        {
            if (_hiddenChildren != null)
            {
                return;
            }
            ContextOnItem = true;

            List<TemplateTreeViewItem<ABLevelTemplate>> selectedItems = new List<TemplateTreeViewItem<ABLevelTemplate>>();
            foreach (var nodeID in GetSelection())
            {
                selectedItems.Add(FindItem(nodeID, rootItem) as TemplateTreeViewItem<ABLevelTemplate>);
            }
            GenericMenu menu = new GenericMenu();
            if (selectedItems.Count == 1)
            {
                menu.AddItem(new GUIContent("Удалить " + selectedItems[0].displayName), false, DeleteGroups, selectedItems);
            }
            else if (selectedItems.Count > 1)
            {
                menu.AddItem(new GUIContent("Удалить " + selectedItems.Count + " выбранные темплейты"), false, DeleteGroups, selectedItems);
            }
            menu.ShowAsContext();
        }
        protected override bool CanRename(TreeViewItem item)
        {
            if (item == _hiddenChildren)
            {
                return false;
            }
            return true;
        }
        protected override void RenameEnded(RenameEndedArgs args)
        {
            if (_hiddenChildren != null)
            {
                if (args.acceptedRename)
                {
                    var remanedItem = FindItem(args.itemID, rootItem) as TemplateTreeViewItem<ABLevelTemplate>;
                    if (string.IsNullOrEmpty(args.newName))
                    {
                        remanedItem.Item.Name = args.originalName;
                    }
                    else
                    {
                        remanedItem.Item.Name = args.newName;
                    }
                }
            }
            
        }
        private void CreateNewItem()
        {
            //
            Refresh();
        }
        private void DeleteGroups(object groups)
        {
            var selectedItems = groups as List<TemplateTreeViewItem<ABLevelTemplate>>;
            foreach (var item in selectedItems)
            {
            }
            Refresh();
        }
    }
}

