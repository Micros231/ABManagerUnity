using ABManagerCore.Enums;
using ABManagerEditor.BuildModels;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Settings
{
    public class ManagerSettings : ScriptableObject
    {
        public string Version { get => _version; set => _version = value; }
        public string BuildPath { get => _buildPath; set => _buildPath = value; }
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
        public BuildCompressionType BuildCompressionType
        {
            get => _buildCompressionType;
            set => _buildCompressionType = value;
        }
        public BuildCompression Compression
        {
            get
            {
                BuildCompression compression;
                switch (BuildCompressionType)
                {
                    case BuildCompressionType.LZMA:
                        compression = BuildCompression.LZMA;
                        break;
                    case BuildCompressionType.LZ4:
                        compression = BuildCompression.LZ4;
                        break;
                    case BuildCompressionType.Uncompressed:
                        compression = BuildCompression.Uncompressed;
                        break;
                    default:
                        compression = BuildCompression.LZMA;
                        break;
                }
                return compression;
            }
        }
        public IList<ABLevelTemplate> LevelTemplates => _levelTemplates;
        public IList<ABStandTemplate> StandTemplates => _standTepmplates;
        public IList<ABInstanceTemplate> InstanceTemplates => _instanceTemplates;


        [SerializeField]
        private BuildTargetGroup _buildTargetGroup;
        [SerializeField]
        private BuildTarget _buildTarget;
        [SerializeField]
        private string _version;
        [SerializeField]
        private string _buildPath;
        [SerializeField]
        private BuildCompressionType _buildCompressionType = BuildCompressionType.LZMA;
        [SerializeField]
        private List<ABLevelTemplate> _levelTemplates = new List<ABLevelTemplate>();
        [SerializeField]
        private List<ABStandTemplate> _standTepmplates = new List<ABStandTemplate>();
        [SerializeField]
        private List<ABInstanceTemplate> _instanceTemplates = new List<ABInstanceTemplate>();
    }
}


