using ABManagerCore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class ABManifest
    {
        public string Version 
        {
            get => _version; 
            internal set => _version = value; 
        }
        public RuntimePlatform PlatformTarget 
        { 
            get => _platformTarget.ToEnum<RuntimePlatform>(); 
            internal set => _platformTarget = value.ToString(); 
        }
        public List<LevelTemplateInfo> LevelTemplatesInfo => _levelTemplatesInfo;
        public List<StandTemplateInfo> StandTemplatesInfo => _standTemplatesInfo;
        public List<InstanceTemplateInfo> InstanceTemplatesInfo => _instanceTemplatesInfo;

        [SerializeField]
        private string _version;
        [SerializeField]
        private string _platformTarget;
        [SerializeField]
        private List<LevelTemplateInfo> _levelTemplatesInfo = new List<LevelTemplateInfo>();
        [SerializeField]
        private List<StandTemplateInfo> _standTemplatesInfo = new List<StandTemplateInfo>();
        [SerializeField]
        private List<InstanceTemplateInfo> _instanceTemplatesInfo = new List<InstanceTemplateInfo>();
    }
}

