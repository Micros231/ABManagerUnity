using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Controller;
using ABManagerCore.Settings;

namespace ABManagerEditor.Browser
{
    public class ABBrowser : EditorWindow
    {

        [MenuItem("Window/ABManagerV2.0")]
        public static void ShowBrowser()
        {
            ABBrowser browser = GetWindow<ABBrowser>("ABManager", true);
            browser.Show();
        }
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
            if (ABController.Current.HostSettings != null)
            {
                ABController.Current.Saver.SaveHostSettings();
            }
            if (ABController.Current.ManagerSettings!= null)
            {
                ABController.Current.Saver.SaveManagerSettings();
            }
        }
        private void OnGUI()
        {
            if (ABController.Current.HostSettings == null)
            {
                GUILayout.Label("Нет HOSTSettings!");
                if (GUILayout.Button("Создать HOSTSettings"))
                {
                    ABController.Current.Creators.HostSettingsCreator.Create();
                }
                
            }
            else
            {
                ABController.Current.HostSettings.URLHost = EditorGUILayout.TextField("UrlHost: ", ABController.Current.HostSettings.URLHost);
            }
            if (ABController.Current.ManagerSettings == null)
            {
                GUILayout.Label("Нет ManagerSettings!");
                if (GUILayout.Button("Создать ManagerSettings"))
                {
                    ABController.Current.Creators.ManagerSettingsCreator.Create();
                }
            }
            else
            {
                ABController.Current.ManagerSettings.Version = EditorGUILayout.TextField("Version: ", ABController.Current.ManagerSettings.Version);
                ABController.Current.ManagerSettings.BuildPath = EditorGUILayout.TextField("BuildPAth: ", ABController.Current.ManagerSettings.BuildPath);
                ABController.Current.ManagerSettings.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("BuildTarget:", ABController.Current.ManagerSettings.BuildTarget);
            }
            if (GUILayout.Button("Test Build"))
            {
                ABController.Current.Builder.Build();
            }
        }
    }
}

