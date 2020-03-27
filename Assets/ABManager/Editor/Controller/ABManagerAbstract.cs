using ABManagerEditor.Consts;
using ABManagerEditor.Models;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Controller
{
    internal abstract class ABManagerAbstract
    {
        internal ABSettings Settings
        {
            get
            {
                if (_settings != null)
                    return _settings;
                if (File.Exists(ABPaths.SettingsFilePath))
                {
                    _settings = AssetDatabase.LoadAssetAtPath<ABSettings>(ABPaths.SettingsFilePath);
                }
                if (_settings == null)
                    Debug.LogWarning("Не найден файл настроек, пожалуйста создайте его, чтобы использовать!");
                return _settings;

            }
        }
        private ABSettings _settings;
    }
}

