using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Controller;
using ABManagerCore.Consts;
using ABManagerCore.Enums;
using ABManagerEditor.Settings;

namespace ABManagerEditor.Browser.Blocks.Settings
{
    internal class ManagerSettingsBlock : AbstractBlock
    {
        private ManagerSettings _managerSettings;
        internal override void OnEnable()
        {
            if (_managerSettings == null)
            {
                _managerSettings = ABController.Current.ManagerSettings;
            }
        }
        internal override void OnDisable()
        {
            if (_managerSettings != null)
            {
                _managerSettings = null;
            }
        }
        internal override void OnGUI(Rect screenRect)
        {
            if (_managerSettings == null)
            {
                var labelStyle = new GUIStyle();
                labelStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(Messages.GUIMessages.ManagerSettingsNull, labelStyle);
                EditorGUILayout.HelpBox(Messages.GUIMessages.HowCreateSettings, MessageType.Info);
                if (Event.current.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Создать Настройки"), false, CreateSettings);
                    menu.ShowAsContext();
                }
                return;
            }
            _managerSettings.Version = 
                EditorGUILayout.TextField("Версия:", _managerSettings.Version);
            _managerSettings.BuildPath =
                EditorGUILayout.TextField("Путь билда:", _managerSettings.BuildPath);
            _managerSettings.BuildTarget =
                (BuildTarget)EditorGUILayout.EnumPopup("Платформа:", _managerSettings.BuildTarget);
            _managerSettings.BuildCompressionType =
                (BuildCompressionType)EditorGUILayout.EnumPopup("Сжатие:", _managerSettings.BuildCompressionType);
        }

        private void CreateSettings()
        {
            ABController.Current.Creators.ManagerSettingsCreator.Create();
            ABController.Current.Saver.SaveManagerSettings();
            _managerSettings = ABController.Current.ManagerSettings;
        }
    }
}

