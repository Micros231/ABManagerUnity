using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using UnityEngine;
using ABManagerEditor.Controller;
using ABManagerEditor.Models;
using System.IO;
using ABManagerEditor.Interfaces;
using System;

namespace ABManagerEditor.Browser.Blocks
{
    internal class GroupsBlock : AbstractBlock, ISelectABGroup
    {
        public event Action<ABGroup> OnSelectABGroup;

        [SerializeField] 
        private TreeViewState _treeViewState;
        private ABGroupTree _groupTreeView;
        private ABGroup _currentGroup;
        private readonly ABManagerController _controller;
        private Vector2 _groupScrollPosition;

        

        public GroupsBlock(ABManagerController controller)
        {
            _controller = controller;
        }
        internal override void OnEnable()
        {
            if (_treeViewState == null)
                _treeViewState = new TreeViewState();
            _groupTreeView = new ABGroupTree(_treeViewState, _controller);
            _groupTreeView.OnSelectABGroup += OnSelectGroup;
        }
        internal override void OnDisable()
        {
            _groupTreeView.OnSelectABGroup -= OnSelectGroup;
        }
        private void OnSelectGroup(ABGroup selectedGroup)
        {
            _currentGroup = selectedGroup;
            OnSelectABGroup?.Invoke(selectedGroup);
        }
        internal override void OnGUI(Rect screenRect)
        {
            float paddingHeight = 30;
            GUILayout.Label("Группы:",
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
            _groupTreeView.OnGUI(treeRect);
            _groupScrollPosition = GUILayout.BeginScrollView(_groupScrollPosition,GUILayout.Width(screenRect.width));
            GUILayout.Label("Настройка группы:",
                new GUIStyle { alignment = TextAnchor.MiddleCenter },
                GUILayout.Height(paddingHeight),
                GUILayout.Width(screenRect.width));
            if (_currentGroup == null)
            {
                EditorGUILayout.HelpBox("Группа не выбрана", MessageType.Info);
            }
            else
            {
                _currentGroup.Name = EditorGUILayout.TextField("Имя группы:", _currentGroup.Name);
                _currentGroup.Version = EditorGUILayout.TextField("Версия группы:", _currentGroup.Version);
                _currentGroup.IsCustomSettings = EditorGUILayout.Toggle("Кастомные настройки:", _currentGroup.IsCustomSettings);
                _currentGroup.BuildCompressionType = (BuildCompressionType)EditorGUILayout.EnumPopup("Метод сжатия:", _currentGroup.BuildCompressionType);
                if (_currentGroup.IsCustomSettings)
                {
                    _currentGroup.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("Платформа:", _currentGroup.BuildTarget);
                    _currentGroup.BuildPath = EditorGUILayout.TextField("Полный путь билда:", _currentGroup.BuildPath);
                    _currentGroup.LocalLoadPath = EditorGUILayout.TextField("Путь лоакльной загрузки:", _currentGroup.LocalLoadPath);
                    _currentGroup.RemoteLoadPath = EditorGUILayout.TextField("Путь удаленной загрузки:", _currentGroup.RemoteLoadPath);
                    if (_currentGroup.Items != null && _currentGroup.Items.Count > 0)
                    {
                        if (GUILayout.Button("Билд", GUILayout.Width(screenRect.width)))
                        {
                            _controller.Builder.BuildGroup(_currentGroup);
                        }
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Платформа:", _controller.Settings.BuildTarget.ToString());
                    _currentGroup.BuildPath = PathFiled("Продолжение путя билда:", _currentGroup.BuildPath, _controller.Settings.BuildPath);
                    _currentGroup.LocalLoadPath = PathFiled("Продолжение путя лоакльной загрузки:", _currentGroup.LocalLoadPath, _controller.Settings.LocalLoadPath);
                    _currentGroup.RemoteLoadPath = PathFiled("Продолжение путя удаленной загрузки:", _currentGroup.RemoteLoadPath, _controller.Settings.RemoteLoadPath);
                }
            }
            GUILayout.EndScrollView();
            
        }
        public string PathFiled(string name, string path, string settingsPath = null)
        {
            string newPath = EditorGUILayout.TextField(name, path);
            EditorGUILayout.TextArea($"{settingsPath}/{path}");
            return newPath;
        }
    }
}

