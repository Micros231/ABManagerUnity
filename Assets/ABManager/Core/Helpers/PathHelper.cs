using System;
using System.IO;
using UnityEngine;

namespace ABManagerCore.Helpers
{
    public static class PathHelper
    {
        public static bool IsValidLocalPath(string localPath, string namePath = null)
        {
            if (string.IsNullOrEmpty(localPath))
            {
                Debug.LogError($"{namePath} не может быть пустой строкой или равен null");
                return false;
            }
            if (localPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                Debug.LogError($"{namePath} имеет не доступные знаки");
                return false;
            }
            return true;
        }
        public static bool IsValidRemotePath(string remotePath, string namePath = null)
        {
            if (string.IsNullOrEmpty(remotePath))
            {
                Debug.LogError($"{namePath} не может быть пустой строкой или равен null");
                return false;
            }
            if (!Uri.IsWellFormedUriString(remotePath, UriKind.RelativeOrAbsolute))
            {
                Debug.LogError($"{namePath} {remotePath} не корректен!");
                return false;
            }
            return true;
        }
    }
}

