using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Consts;

namespace ABManagerCore.Helpers
{
    public static class FileNameHelper
    {
        public static string GetAssetBundleFileName(string nameTemplate, string typeContent, string name)
        {
            return $"{nameTemplate}_{typeContent}({name})" + FileExtensions.AssetBundle;
        }
    }
}

