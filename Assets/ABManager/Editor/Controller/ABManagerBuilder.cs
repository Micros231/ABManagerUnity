using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Build.Pipeline;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using ABManagerEditor.Models;
using ABManagerCore;
using UnityEditor.Build.Pipeline.Tasks;

namespace ABManagerEditor.Controller
{
    internal class ABManagerBuilder : ABManagerAbstract 
    {
        private ABManifest _currentManifest;
        internal ABManagerBuilder()
        {
            ContentPipeline.BuildCallbacks.PostWritingCallback = PostWriting;
        }
        internal void BuildAll()
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Setttings not found", "ABSettings.asset");
            }
            if (Settings.Items == null)
            {
                throw new NullReferenceException("Groups is null");
            }
            var groupsToBuild = Settings.Items.Where(group => !group.IsCustomSettings);
            if (groupsToBuild == null)
            {
                throw new NullReferenceException("GroupsToBuild is null");
            }
            foreach (var group in groupsToBuild)
            {
                BuildGroup(group);
            }
            Debug.Log("Build All success");
        }
        internal void BuildGroup(ABGroup group)
        {
            if (Settings == null)
            {
                throw new FileNotFoundException("Setttings not found", "ABSettings.asset");
            }
            if (group == null)
            {
                throw new NullReferenceException("SimpleGroupAssetBundles is null");
            }
            string buildPath = string.Empty;
            string localLoadPath = string.Empty;
            string remoteLoadPath = string.Empty;
            BuildTarget buildTarget = BuildTarget.StandaloneWindows;
            BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone;
            if (group.IsCustomSettings)
            {
                buildPath = group.BuildPath;
                localLoadPath = group.LocalLoadPath;
                remoteLoadPath = group.RemoteLoadPath;
                buildTarget = group.BuildTarget;
                buildTargetGroup = group.BuildTargetGroup;
            }
            else
            {
                buildPath = $"{Settings.BuildPath}/{group.BuildPath}";
                localLoadPath = $"{Settings.LocalLoadPath}/{group.LocalLoadPath}";
                remoteLoadPath = $"{Settings.RemoteLoadPath}/{group.RemoteLoadPath}";
                buildTarget = Settings.BuildTarget;
                buildTargetGroup = Settings.BuildTargetGroup;
            }
            if (buildPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new DirectoryNotFoundException("BuildPath invalid chars");
            }
            if (localLoadPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new DirectoryNotFoundException("LocalLoadPath invalid chars");
            }
            if (remoteLoadPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new DirectoryNotFoundException("LocalLoadPath invalid chars");
            }
            if (group.BundleBuilds == null || group.BundleBuilds.Count() <= 0)
            {
                Debug.LogWarning($"Бандлы для билда группы {group.Name} являются null или их нет. Билд этой группы не будет производиться");
                return;
            }
            if (!Directory.Exists(buildPath))
            {
                try
                {
                    Directory.CreateDirectory(buildPath);
                }
                catch (IOException ex)
                {
                    throw ex;
                    throw new IOException("Путь билда указывает на файл");
                }
                catch(NotSupportedException ex)
                {
                    throw ex;
                    throw new NotSupportedException("Путь билда содержит двоеточие (:), которое не является частью метки");
                }
            }
            _currentManifest = new ABManifest();
            _currentManifest.Name = group.Name;
            _currentManifest.Version = group.Version;
            _currentManifest.LocalLoadPath = localLoadPath;
            _currentManifest.RemoteLoadPath = remoteLoadPath;
            foreach (var item in group.BundleBuilds)
            {
                _currentManifest.Bundles.Add(new BundleInfo { Name = item.assetBundleName });
            }
            var parameters = new BundleBuildParameters(buildTarget, buildTargetGroup, buildPath);
            parameters.BundleCompression = group.Compression;
            var buildContent = new BundleBuildContent(group.BundleBuilds);
            foreach (var item in buildContent.Assets)
            {
                _currentManifest.Assets.Add(new AssetInfo()
                {
                    Name = buildContent.Addresses[item],
                    Path = AssetDatabase.GUIDToAssetPath(item.ToString())
                });
            }
            foreach (var item in buildContent.Scenes)
            {
                _currentManifest.Scenes.Add(new AssetInfo()
                {
                    Name = buildContent.Addresses[item],
                    Path = AssetDatabase.GUIDToAssetPath(item.ToString())
                });
            }
            IBundleBuildResults results;
            var returnCode = ContentPipeline.BuildAssetBundles(parameters, buildContent, out results, new[] { new ArchiveAndCompressBundles() });
            if (returnCode == ReturnCode.Success)
            {
                string jsonContent = JsonUtility.ToJson(_currentManifest);
                using (var sw = File.CreateText(Path.Combine(buildPath, $"manifest-{group.Name}.json")))
                {
                    sw.Write(jsonContent);
                }
            }
        }
        private ReturnCode PostWriting(IBuildParameters buildParameters, IDependencyData dependencyData, IWriteData writeData, IBuildResults results)
        {
            var parameters = buildParameters as BundleBuildParameters;
            return ReturnCode.Success;
        }
    }
}

