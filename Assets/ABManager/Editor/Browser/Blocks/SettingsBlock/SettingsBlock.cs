using ABManagerEditor.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Controller;

namespace ABManagerEditor.Browser.Blocks
{
    internal class SettingsBlock : AbstractBlock
    {
        private Vector2 _scrollPosition;
        private readonly ABManagerController _controller;
        internal SettingsBlock(ABManagerController controller)
        {
            _controller = controller;
        }
        internal override void OnGUI(Rect position)
        {
            
            GUILayout.Label("Ваши настройки ABManager:", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            _controller.Settings.Version = EditorGUILayout.TextField("Версия глобального манифеста:", _controller.Settings.Version);
            _controller.Settings.BuildPath = EditorGUILayout.TextField("Общий путь билда:", _controller.Settings.BuildPath);
            _controller.Settings.LocalLoadPath = EditorGUILayout.TextField("Общий путь локальной загрузки:", _controller.Settings.LocalLoadPath);
            _controller.Settings.RemoteLoadPath = EditorGUILayout.TextField("Общий путь удаленной загрузки:", _controller.Settings.RemoteLoadPath);
            _controller.Settings.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("Платформа:", _controller.Settings.BuildTarget);
            GUILayout.EndScrollView();
            
        }
    }
}

