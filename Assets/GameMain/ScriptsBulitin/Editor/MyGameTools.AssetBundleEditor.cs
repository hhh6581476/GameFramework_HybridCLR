using GameFramework;
using GameFramework.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor;
using UnityGameFramework.Editor.ResourceTools;
public partial class MyGameTools
{
    [MenuItem("Game Framework/Resource Tools/Refresh Shared Assets【解决资源重复依赖】", false, 100)]
    public static void RefreshABDependencyAssets()
    {
        ResourceEditorController resEditor = new ResourceEditorController();

        resEditor.OnLoadCompleted += () =>
        {
            if (resEditor.HasResource(ConstEditor.SharedAssetBundleName, null))
            {
                resEditor.RemoveResource(ConstEditor.SharedAssetBundleName, null);
                resEditor.Save();
            }

            ResourceAnalyzerController resAnalyzer = new ResourceAnalyzerController();
            resAnalyzer.OnAnalyzeCompleted += () =>
            {
                var duplicateAssetNames = FindDuplicateAssetNames(resEditor, resAnalyzer);
                RefreshSharedAssetBundle(resEditor, duplicateAssetNames);
                //Debug.Log($">>>>>>>>>>>>>>>>>>>>Duplicate Asset Count:{duplicateAssetNames.Length}");
            };
            resAnalyzer.Prepare();
            resAnalyzer.Analyze();
        };
        resEditor.Load();
    }
    private static void RefreshSharedAssetBundle(ResourceEditorController resEditor, string[] duplicateAssetNames)
    {
        if (duplicateAssetNames == null || duplicateAssetNames.Length < 1)
        {
            return;
        }

        if (!resEditor.HasResource(ConstEditor.SharedAssetBundleName, null))
        {
            bool addSuccess = resEditor.AddResource(ConstEditor.SharedAssetBundleName, null, null, LoadType.LoadFromMemoryAndQuickDecrypt, false);

            if (!addSuccess)
            {
                Debug.LogWarningFormat("ResourceEditor Add Resource:{0} Failed!", ConstEditor.SharedAssetBundleName);
                return;
            }
        }

        var sharedRes = resEditor.GetResource(ConstEditor.SharedAssetBundleName, null);

        sharedRes.Clear();
        bool hasChanged = false;
        foreach (var assetName in duplicateAssetNames)
        {
            if (resEditor.AssignAsset(AssetDatabase.AssetPathToGUID(assetName), sharedRes.Name, sharedRes.Variant))
            {
                hasChanged = true;
            }
        }

        if (hasChanged)
        {
            resEditor.RemoveUnknownAssets();
            resEditor.RemoveUnusedResources();
            resEditor.Save();
        }
    }
    private static string[] FindDuplicateAssetNames(ResourceEditorController resEditor, ResourceAnalyzerController resAnalyzer)
    {
        var abResArr = resEditor.GetResources();
        Dictionary<string, int> duplicateAssetDic = new Dictionary<string, int>();

        foreach (var item in abResArr)
        {
            if (item.Name.CompareTo(ConstEditor.SharedAssetBundleName) == 0)
            {
                continue;
            }

            List<string> resDepAssets = FindDependencyAssets(resEditor, resAnalyzer, item);
            resDepAssets.Distinct();
            foreach (var resDepName in resDepAssets)
            {
                if (!duplicateAssetDic.ContainsKey(resDepName))
                {
                    duplicateAssetDic.Add(resDepName, 1);
                }
                else
                {
                    duplicateAssetDic[resDepName]++;
                }
            }
        }
        string[] result = new string[0];
        foreach (var item in duplicateAssetDic)
        {
            if (item.Value > 1)
            {
                ArrayUtility.Add(ref result, item.Key);
            }
        }
        return result;
    }

    private static List<string> FindDependencyAssets(ResourceEditorController resEditor, ResourceAnalyzerController resAnalyzer, Resource resNode)
    {
        List<string> result = new List<string>();
        var abChildAssets = resEditor.GetAssets(resNode.Name, resNode.Variant);
        foreach (var abChildAsset in abChildAssets)
        {
            var depAssets = resAnalyzer.GetDependencyData(abChildAsset.Name);
            if (depAssets == null)
            {
                if (!result.Contains(abChildAsset.Name))
                {
                    result.Add(abChildAsset.Name);
                }
                continue;
            }

            foreach (var resName in depAssets.GetScatteredDependencyAssetNames())
            {
                if (!result.Contains(resName))
                {
                    result.Add(resName);
                }
            }
        }

        return result;
    }
}
