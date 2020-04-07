using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace ABManagerCore.Manifest.Builder
{
    public class ABManifestBuilder
    {
        private readonly ABManifest _manifest;

        public ABManifestBuilder()
        {
            _manifest = new ABManifest();
        }

        public ABManifestBuilder Version(string version)
        {
            _manifest.Version = version;
            return this;
        }
#if UNITY_EDITOR
        public ABManifestBuilder PlatformTarget(BuildTarget buildTarget)
        {
            RuntimePlatform platform = RuntimePlatform.WindowsPlayer; 
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                    platform = RuntimePlatform.OSXPlayer;
                    break;
                case BuildTarget.StandaloneWindows:
                    platform = RuntimePlatform.WindowsPlayer;
                    break;
                case BuildTarget.iOS:
                    platform = RuntimePlatform.IPhonePlayer;
                    break;
                case BuildTarget.Android:
                    platform = RuntimePlatform.Android;
                    break;
                case BuildTarget.StandaloneWindows64:
                    platform = RuntimePlatform.WindowsPlayer;
                    break;
                case BuildTarget.WebGL:
                    platform = RuntimePlatform.WebGLPlayer;
                    break;
                case BuildTarget.StandaloneLinux64:
                    platform = RuntimePlatform.LinuxPlayer;
                    break;
                case BuildTarget.PS4:
                    platform = RuntimePlatform.PS4;
                    break;
                case BuildTarget.XboxOne:
                    platform = RuntimePlatform.XboxOne;
                    break;
                case BuildTarget.tvOS:
                    platform = RuntimePlatform.tvOS;
                    break;
                case BuildTarget.Switch:
                    platform = RuntimePlatform.Switch;
                    break;
                case BuildTarget.Lumin:
                    platform = RuntimePlatform.Lumin;
                    break;
                case BuildTarget.BJM:
                    platform = RuntimePlatform.BJM;
                    break;
                default:
                    platform = RuntimePlatform.WindowsPlayer;
                    break;
            }
            _manifest.PlatformTarget = platform;
            return this;
        }
#endif

        public LevelTemplateBuilder NewLevelTemplate()
        {
            return new LevelTemplateBuilder(this, _manifest);
        }
        public StandTemplateBuilder NewStandTemplate()
        {
            return new StandTemplateBuilder(this, _manifest);
        }
        public InstanceTemplateBuilder NewInstanceTemplate()
        {
            return new InstanceTemplateBuilder(this, _manifest);
        }
        public ABManifest Build()
        {
            return _manifest;
        }
    }
}

