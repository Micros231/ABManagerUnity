using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore
{
    [Serializable]
    public class ABManifest
    {
        public string Name;
        public string Version;
        public string LocalLoadPath;
        public string RemoteLoadPath;
        public List<BundleInfo> Bundles = new List<BundleInfo>();
        public List<AssetInfo> Assets = new List<AssetInfo>();
        public List<AssetInfo> Scenes = new List<AssetInfo>();

        public override string ToString()
        {
            string str =
                $"Version: {Version} {Environment.NewLine}" +
                $"LocalLoadPath: {LocalLoadPath} {Environment.NewLine}" +
                $"RemoteLoadPath: {RemoteLoadPath} {Environment.NewLine}" +
                $"Bundles: {Environment.NewLine}";
            foreach (var item in Bundles)
            {
                str += $"BunleName: {item.Name} {Environment.NewLine}";
            }
            str += $"Assets: {Environment.NewLine}";
            foreach (var item in Assets)
            {
                str += 
                    $"AssetName: {item.Name} {Environment.NewLine}" +
                    $"AssetPath: {item.Path} {Environment.NewLine}";
            }
            str += $"Scenes: {Environment.NewLine}";
            foreach (var item in Scenes)
            {
                str +=
                    $"SceneName: {item.Name} {Environment.NewLine}" +
                    $"ScenePath: {item.Path} {Environment.NewLine}";
            }
            return str;
        }
    }
}

