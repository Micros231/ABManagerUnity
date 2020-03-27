using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Helpers;
using UnityEditor;

namespace ABManagerEditor.Models
{
    public class ABSettings : ABAbstract<ABGroup>
    {
        public override string BuildPath 
        {
            get => _buildPath;
            set
            {
                if (PathHelper.IsValidLocalPath(value, "Общий билд путь"))
                {
                    _buildPath = value;
                }
            }
        }
        public override string LocalLoadPath 
        { 
            get => _localLoadPath; 
            set
            {
                if (PathHelper.IsValidLocalPath(value, "Общий локальный путь загрузки"))
                {
                    _localLoadPath = value;
                }
            } 
        }
        public override string RemoteLoadPath 
        {
            get => _remoteLoadPath; 
            set
            {
                if (PathHelper.IsValidRemotePath(value, "Общий удаленный путь загрузки"))
                {
                    _remoteLoadPath = value;
                }
            }
        }
    }
}

