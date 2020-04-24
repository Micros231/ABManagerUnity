
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using ABManagerCore.Consts;
using ABManagerEditor.Controller;
using ABManagerCore.Settings;

namespace ABManagerEditor.Browser.Blocks.Settings
{
    internal class HostSettingsBlock : AbstractBlock
    {
        private HostSettings _hostSettings;
        internal override void OnEnable()
        {
            if (_hostSettings == null)
            {
                _hostSettings = ABController.Current.HostSettings;
            }
        }
        internal override void OnDisable()
        {
            if (_hostSettings != null)
            {
                _hostSettings = null;
            }
        }
        internal override void OnGUI(Rect screenRect)
        {
            if (_hostSettings == null)
            {
                var labelStyle = new GUIStyle();
                labelStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(Messages.GUIMessages.HostSettingsNull, labelStyle);
                EditorGUILayout.HelpBox(Messages.GUIMessages.HowCreateSettings, MessageType.Info);
                if (Event.current.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Создать Настройки"), false, CreateSettings);
                    menu.ShowAsContext();
                }
                return;
            }
            _hostSettings.URLHost =
                EditorGUILayout.TextField("URL хоста:", _hostSettings.URLHost);
            GUILayout.Space(Sizes.Spaces.space_15);
            APIArea();
        }
        
        private void APIArea()
        {
            GUILayout.Label("APIs:");
            GUILayout.Space(Sizes.Spaces.space_15);
            GUILayout.Label("GET APIs:");
            TextAPI("GetCurrentVersion:", RelativeURLs.GetCurrentVersion);
            TextAPI("GetCurrentManifestInfo:", RelativeURLs.GetCurrentManifestInfo);
            TextAPIFormat("GetManifestInfoByVersionFormat:", RelativeURLs.GetManifestInfoByVersionFormat, "{version}");
            GUILayout.Space(Sizes.Spaces.space_15);
            GUILayout.Label("Dodwnload APIs:");
            TextAPI("DownloadCurrentManifest:", RelativeURLs.DownloadCurrentManifest);
            TextAPIFormat("DownloadManifestByVersionFormat:", RelativeURLs.DownloadManifestByVersionFormat, "{version}");
            GUILayout.Space(Sizes.Spaces.space_15);
            GUILayout.Label("Upload APIs:");
            TextAPI("UploadManifest:", RelativeURLs.UploadManifest);
        }
        private void TextAPI(string name, string relativeURL)
        {
            var urlHost = _hostSettings.URLHost;
            EditorGUILayout.TextField(name, urlHost + relativeURL);
        }
        private void TextAPIFormat(string name, string relativeURL, string arg1)
        {
            var urlHost = _hostSettings.URLHost;
            var relativeUrlFormat = string.Format(relativeURL, arg1);
            EditorGUILayout.TextField(name, urlHost + relativeUrlFormat);
        }
        private void TextAPIFormat(string name, string relativeURL, params string[] args)
        {
            var urlHost = _hostSettings.URLHost;
            var relativeUrlFormat = string.Format(relativeURL, args);
            EditorGUILayout.TextField(name, urlHost + relativeUrlFormat);
        }
        private void CreateSettings()
        {
            ABController.Current.Creators.HostSettingsCreator.Create();
            ABController.Current.Saver.SaveHostSettings();
            _hostSettings = ABController.Current.HostSettings;
        }
    }
}

