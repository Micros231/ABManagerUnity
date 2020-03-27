using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Models
{
    public abstract class ABAbstract<T> : ScriptableObject
    {
        public string Version
        {
            get => _version;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Debug.LogWarning($"Версия {name} не может быть пустой строкой или null");
                    _version = "0";
                }
                else
                {
                    _version = value;
                }
            }
        }
        public abstract string BuildPath { get; set; }
        public abstract string LocalLoadPath { get; set; }
        public abstract string RemoteLoadPath { get; set; }
        public BuildTarget BuildTarget
        {
            get => _buildTarget;
            set
            {
                switch (value)
                {
                    case BuildTarget.StandaloneOSX:
                        _buildTargetGroup = BuildTargetGroup.Standalone;
                        break;
                    case BuildTarget.StandaloneWindows:
                        _buildTargetGroup = BuildTargetGroup.Standalone;
                        break;
                    case BuildTarget.iOS:
                        _buildTargetGroup = BuildTargetGroup.iOS;
                        break;
                    case BuildTarget.StandaloneWindows64:
                        _buildTargetGroup = BuildTargetGroup.Standalone;
                        break;
                    case BuildTarget.WebGL:
                        _buildTargetGroup = BuildTargetGroup.WebGL;
                        break;
                    case BuildTarget.WSAPlayer:
                        _buildTargetGroup = BuildTargetGroup.WSA;
                        break;
                    case BuildTarget.StandaloneLinux64:
                        _buildTargetGroup = BuildTargetGroup.Standalone;
                        break;
                    case BuildTarget.tvOS:
                        _buildTargetGroup = BuildTargetGroup.tvOS;
                        break;
                    case BuildTarget.Switch:
                        _buildTargetGroup = BuildTargetGroup.Switch;
                        break;
                    case BuildTarget.Lumin:
                        _buildTargetGroup = BuildTargetGroup.Lumin;
                        break;
                    case BuildTarget.BJM:
                        _buildTargetGroup = BuildTargetGroup.BJM;
                        break;
                    case BuildTarget.NoTarget:
                        _buildTargetGroup = BuildTargetGroup.Unknown;
                        break;
                }
                _buildTarget = value;
            }
        }
        public BuildTargetGroup BuildTargetGroup
        {
            get => _buildTargetGroup;
        }
        public IList<T> Items => _items;

        [SerializeField]
        protected string _buildPath;
        [SerializeField]
        protected string _localLoadPath;
        [SerializeField]
        protected string _remoteLoadPath;

        [SerializeField]
        private string _version;
        [SerializeField]
        private BuildTarget _buildTarget = BuildTarget.StandaloneWindows;
        [SerializeField]
        private BuildTargetGroup _buildTargetGroup = BuildTargetGroup.Standalone;
        [SerializeField]
        private List<T> _items = new List<T>();

    }
}

