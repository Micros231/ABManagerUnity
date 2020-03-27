using System.IO;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Models;
using ABManagerEditor.Consts;
using ABManagerEditor.Browser.Blocks;
using ABManagerEditor.Controller;

namespace ABManagerEditor.Browser
{
    public class ManagerBrowser : EditorWindow
    {
        private ABManagerController _controller;
        private MainBlock _mainBlock;

        [MenuItem("Window/" + ABNames.Manager)]
        private static void ShowWindow()
        {
            ManagerBrowser window = GetWindow<ManagerBrowser>();
            window.titleContent = new GUIContent(ABNames.Manager);
            window.Show();
        }
        private void OnEnable()
        {
            if (_controller == null)
                _controller = new ABManagerController();
            if (_mainBlock == null)
                _mainBlock = new MainBlock(_controller);
            if (_controller.Settings != null)
            {
                _mainBlock.OnEnable();
            }
            
        }
        private void OnDisable()
        {
            if (_mainBlock != null)
                _mainBlock.OnDisable();
            _controller.SaveSettings();
        }
        private void OnGUI()
        {
            Rect screenRect = new Rect(0, 0, position.width, position.height);
            if (_controller.Settings == null)
            {
                GUILayout.Label("Не найдены главные настройки, чтобы продолжить работу, создайте их, нажав ПКМ и выбрав пункт \"Создать Настройки\"",
                    new GUIStyle { alignment = TextAnchor.MiddleCenter });
                if (Event.current.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Создать Настройки"), false, CreateSettings);
                    menu.ShowAsContext();
                }
            }
            else
            {
                _mainBlock.OnGUI(screenRect);
            }        
        }
        private void CreateSettings()
        {
            _controller.Creators.SettingsCreator.Create();
            _controller.SaveSettings();
            if (_mainBlock == null)
                _mainBlock = new MainBlock(_controller);
            if (_controller.Settings != null)
            {
                _mainBlock.OnEnable();
            }
        }
    }
}

