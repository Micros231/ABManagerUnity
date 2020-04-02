using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Controller;

namespace ABManagerEditor.Browser
{
    public class ABBrowser : EditorWindow
    {

        private ABController _controller;
        [MenuItem("Window/ABManagerV2.0")]
        public static void ShowBrowser()
        {
            ABBrowser browser = GetWindow<ABBrowser>("ABManager", true);
            browser.Show();
        }
        private void OnEnable()
        {
            if (_controller == null)
                _controller = new ABController();
        }
        private void OnGUI()
        {
            GUILayout.Label("123");
            if (GUILayout.Button("TestBuild"))
                _controller.Builder.Build();
        }
    }
}

