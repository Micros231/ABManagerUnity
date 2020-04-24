using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using ABManagerCore.Consts;
using UnityEditor;

namespace ABManagerEditor.Browser.Blocks.Manager
{
    internal abstract class AbstractTreeInfoBlock<TItem, TTreeView> : AbstractBlock
        where TTreeView : AbstractTreeView<TItem>
    {
        public TItem CurrentItem { get; private set; }
        protected TreeViewState TreeViewState;
        protected AbstractTreeView<TItem> TreeView;
        protected Rect BlockRect;
        private Vector2 _scrollViewPosition;
        internal override void OnEnable()
        {
            if (TreeViewState == null)
            {
                TreeViewState = new TreeViewState();
            }
            InitializeTreeView();
            if (TreeView == null)
            {
                throw new NullReferenceException("TreeView is null");
            }
            if (!(TreeView is TTreeView))
            {
                throw new InvalidCastException($"TreeView if not {typeof(TTreeView)}");
            }
            TreeView.OnSelectItem += OnSelectItem;
        }
        internal override void OnDisable()
        {
            TreeView.OnSelectItem -= OnSelectItem;
        }
        internal override void OnGUI(Rect screenRect)
        {
            BlockRect = screenRect;
            float treeViewBlockHeight = screenRect.height * 0.6f;
            var treeViewBlockSize = new Vector2(screenRect.width, treeViewBlockHeight);
            var treeViewBlockPosition = new Vector2(screenRect.x, screenRect.y);
            var treeViewBlockRect = new Rect(treeViewBlockPosition, treeViewBlockSize);
            GUILayout.Label(OnNameTreeViewGUI(), 
                new GUIStyle() { alignment = TextAnchor.MiddleCenter });
            var treeViewPositionY = BlockRect.y + Sizes.Spaces.space_15 + Sizes.Spaces.space_15;
            var treeViewHeight = treeViewBlockHeight - treeViewPositionY - Sizes.Spaces.space_15;
            var treeViewPosition = new Vector2(treeViewBlockRect.x, treeViewPositionY);
            var treeViewSize = new Vector2(treeViewBlockRect.width, treeViewHeight);
            var treeViewRect = new Rect(treeViewPosition, treeViewSize);
            TreeView.OnGUI(treeViewRect);
            GUILayout.Space(treeViewHeight);
            OnPostTreeViewGUI();
            var selectedItemBlockPositionY = screenRect.y + treeViewBlockHeight;
            var selectedItemBlockHeight = screenRect.height - treeViewBlockHeight;
            var selectedItemBlockPosition = new Vector2(screenRect.x, selectedItemBlockPositionY);
            var selectedItemBlockSize = new Vector2(screenRect.width, selectedItemBlockHeight);
            var selectedItemBlockRect = new Rect(selectedItemBlockPosition, selectedItemBlockSize);
            GUILayout.BeginArea(selectedItemBlockRect);
            GUILayout.Label("Настройка выбранного объекта:",
                new GUIStyle { alignment = TextAnchor.MiddleCenter },
                GUILayout.Height(Sizes.Heights.height_15),
                GUILayout.Width(screenRect.width));
            _scrollViewPosition = GUILayout.BeginScrollView(_scrollViewPosition, GUILayout.Width(screenRect.width));
            if (CurrentItem == null)
            {
                EditorGUILayout.HelpBox("Объект не выбран!", MessageType.Info);
            }
            else
            {
                GUILayout.BeginVertical(GUILayout.MaxWidth(screenRect.width));
                OnCurrentItmeIsSelectedGUI();
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
        protected abstract void InitializeTreeView();
        protected virtual void CompleteSelectItem(TItem obj)
        {

        }
        protected virtual string OnNameTreeViewGUI()
        {
            return string.Empty;
        }
        protected virtual void OnPostTreeViewGUI()
        {

        }
        protected virtual void OnCurrentItmeIsSelectedGUI()
        {

        }
        private void OnSelectItem(TItem obj)
        {
            CurrentItem = obj;
            CompleteSelectItem(obj);

        }
    }
}

