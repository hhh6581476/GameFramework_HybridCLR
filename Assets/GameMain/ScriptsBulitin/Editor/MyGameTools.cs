#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GameFramework;
//using OfficeOpenXml;
//using GameFramework.Editor.DataTableTools;

//using PathCreation;
using System;

public partial class MyGameTools : EditorWindow
{
    const string DISABLE_HYBRIDCLR = "DISABLE_HYBRIDCLR";
    private static Font toFont;
    private static TMP_FontAsset tmFont;
    private static TMP_SpriteAsset fontSpAsset;

    [MenuItem("Game Framework/GameTools/Tools Window", false, 1000)]
    public static void ShowWin()
    {
        EditorWindow.GetWindow<MyGameTools>().Show();
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        toFont = (Font)EditorGUILayout.ObjectField(new GUIContent("Font:"), toFont, typeof(Font), true, GUILayout.MinWidth(100f));
        tmFont = (TMP_FontAsset)EditorGUILayout.ObjectField(new GUIContent("TMP_FontAsset:"), tmFont, typeof(TMP_FontAsset), true, GUILayout.MinWidth(100f));
        fontSpAsset = (TMP_SpriteAsset)EditorGUILayout.ObjectField(new GUIContent("TMP_SpriteAsset:"), fontSpAsset, typeof(TMP_SpriteAsset), true, GUILayout.MinWidth(100f));
        GUILayout.Space(10);
        if (GUILayout.Button("Replace Font"))
        {
            ReplaceFont();
        }
        GUILayout.Space(30);
        GUILayout.EndVertical();
    }

    [MenuItem("Game Framework/GameTools/Clear Missing Scripts【清除Prefab丢失脚本】")]
    public static void ClearMissingScripts()
    {
        var pfbArr = AssetDatabase.FindAssets("t:Prefab");
        foreach (var item in pfbArr)
        {
            var pfbFileName = AssetDatabase.GUIDToAssetPath(item);
            var pfb = AssetDatabase.LoadAssetAtPath<GameObject>(pfbFileName);
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(pfb);
        }
    }
    //[MenuItem("Game Framework/GameTools/Refresh All Excels【刷新配置表】", false, 1001)]
    //public static async void GenerateDataTables()
    //{
    //    var appConfig = await AppConfigs.GetInstanceSync();
    //    RefreshAllDataTable(appConfig.DataTables);
    //    RefreshAllConfig(appConfig.Configs);

    //    try
    //    {
    //        GenerateUIViewScript();
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogErrorFormat("生成UIView.cs失败:{0}", e.Message);
    //        throw;
    //    }
    //    GenerateGroupEnumScript();
    //    AssetDatabase.Refresh();
    //}
    //public static bool CreateGameConfigExcel(string excelPath)
    //{
    //    try
    //    {
    //        using (var excel = new ExcelPackage(excelPath))
    //        {
    //            var sheet = excel.Workbook.Worksheets.Add("Sheet 1");
    //            sheet.SetValue(1, 1, "#");
    //            sheet.SetValue(1, 2, Path.GetFileNameWithoutExtension(excelPath));
    //            sheet.SetValue(2, 1, "#");
    //            sheet.SetValue(2, 2, "Key");
    //            sheet.SetValue(2, 3, "备注");
    //            sheet.SetValue(2, 4, "Value");
    //            excel.Save();
    //        }
    //        return true;
    //    }
    //    catch (Exception emsg)
    //    {
    //        Debug.LogError($"创建Excel:{excelPath}失败! Error:{emsg}");
    //        return false;
    //    }

    //}
    //public static bool CreateDataTableExcel(string excelPath)
    //{
    //    try
    //    {
    //        using (var excel = new ExcelPackage(excelPath))
    //        {
    //            var sheet = excel.Workbook.Worksheets.Add("Sheet 1");
    //            sheet.SetValue(1, 1, "#");
    //            sheet.SetValue(1, 2, Path.GetFileNameWithoutExtension(excelPath));
    //            sheet.SetValue(2, 1, "#");
    //            sheet.SetValue(2, 2, "ID");
    //            sheet.SetValue(3, 1, "#");
    //            sheet.SetValue(3, 2, "int");
    //            sheet.SetValue(4, 1, "#");
    //            sheet.SetValue(4, 3, "备注");
    //            excel.Save();
    //        }
    //        return true;
    //    }
    //    catch (Exception emsg)
    //    {
    //        Debug.LogError($"创建Excel:{excelPath}失败! Error:{emsg}");
    //        return false;
    //    }

    //}
    /// <summary>
    /// 生成Entity,Sound,UI枚举脚本
    /// </summary>
    //public static void GenerateGroupEnumScript()
    //{
    //    var excelDir = ConstEditor.DataTableExcelPath;
    //    if (!Directory.Exists(excelDir))
    //    {
    //        Debug.LogErrorFormat("Excel DataTable directory is not exists:{0}", excelDir);
    //        return;
    //    }
    //    string[] groupExcels = { ConstEditor.EntityGroupTableExcel, ConstEditor.UIGroupTableExcel, ConstEditor.SoundGroupTableExcel };
    //    StringBuilder sBuilder = new StringBuilder();
    //    sBuilder.AppendLine("public static partial class Const");
    //    sBuilder.AppendLine("{");
    //    foreach (var excel in groupExcels)
    //    {
    //        var excelFileName = UtilityBuiltin.ResPath.GetCombinePath(excelDir, excel);
    //        if (!File.Exists(excelFileName))
    //        {
    //            Debug.LogErrorFormat("Excel is not exists:{0}", excelFileName);
    //            return;
    //        }
    //        var excelPackage = new ExcelPackage(excelFileName);
    //        var excelSheet = excelPackage.Workbook.Worksheets[0];
    //        List<string> groupList = new List<string>();
    //        for (int rowIndex = excelSheet.Dimension.Start.Row; rowIndex <= excelSheet.Dimension.End.Row; rowIndex++)
    //        {
    //            var rowStr = excelSheet.GetValue(rowIndex, 1);
    //            if (rowStr != null && rowStr.ToString().StartsWith("#"))
    //            {
    //                continue;
    //            }
    //            var groupName = excelSheet.GetValue(rowIndex, 4).ToString();
    //            if (!groupList.Contains(groupName)) groupList.Add(groupName);
    //        }
    //        excelSheet.Dispose();
    //        excelPackage.Dispose();

    //        string className = Path.GetFileNameWithoutExtension(excelFileName);
    //        string endWithStr = "Table";
    //        if (className.EndsWith(endWithStr))
    //        {
    //            className = className.Substring(0, className.Length - endWithStr.Length);
    //        }
    //        sBuilder.AppendLine(Utility.Text.Format("\tpublic enum {0}", className));
    //        sBuilder.AppendLine("\t{");
    //        for (int i = 0; i < groupList.Count; i++)
    //        {
    //            if (i < groupList.Count - 1)
    //            {
    //                sBuilder.AppendLine(Utility.Text.Format("\t\t{0},", groupList[i]));
    //            }
    //            else
    //            {
    //                sBuilder.AppendLine(Utility.Text.Format("\t\t{0}", groupList[i]));
    //            }
    //        }
    //        sBuilder.AppendLine("\t}");
    //    }
    //    sBuilder.AppendLine("}");

    //    var outFileName = ConstEditor.ConstGroupScriptFileFullName;
    //    try
    //    {
    //        File.WriteAllText(outFileName, sBuilder.ToString());
    //        Debug.LogFormat("------------------成功生成Group文件:{0}---------------", outFileName);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogErrorFormat("Group文件生成失败:{0}", e.Message);
    //        throw;
    //    }
    //}
    /// <summary>
    /// 生成UI界面枚举类型
    /// </summary>
    //public static void GenerateUIViewScript()
    //{
    //    var excelDir = ConstEditor.DataTableExcelPath;
    //    if (!Directory.Exists(excelDir))
    //    {
    //        Debug.LogError($"生成UIView代码失败! 不存在文件夹:{excelDir}");
    //        return;
    //    }
    //    var excelFileName = UtilityBuiltin.ResPath.GetCombinePath(excelDir, ConstEditor.UITableExcel);
    //    if (!File.Exists(excelFileName))
    //    {
    //        Debug.LogError($"{excelFileName} 文件不存在!");
    //        return;
    //    }
    //    var excelPackage = new ExcelPackage(excelFileName);
    //    var excelSheet = excelPackage.Workbook.Worksheets[0];
    //    Dictionary<int, string> uiViewDic = new Dictionary<int, string>();
    //    for (int rowIndex = excelSheet.Dimension.Start.Row; rowIndex <= excelSheet.Dimension.End.Row; rowIndex++)
    //    {
    //        var rowStr = excelSheet.GetValue(rowIndex, 1);
    //        if (rowStr != null && rowStr.ToString().StartsWith("#"))
    //        {
    //            continue;
    //        }
    //        uiViewDic.Add(int.Parse(excelSheet.GetValue(rowIndex, 2).ToString()), excelSheet.GetValue(rowIndex, 5).ToString());
    //    }
    //    excelSheet.Dispose();
    //    excelPackage.Dispose();
    //    StringBuilder sBuilder = new StringBuilder();
    //    sBuilder.AppendLine("public enum UIViews : int");
    //    sBuilder.AppendLine("{");
    //    int curIndex = 0;
    //    foreach (KeyValuePair<int, string> uiItem in uiViewDic)
    //    {
    //        if (curIndex < uiViewDic.Count - 1)
    //        {
    //            sBuilder.AppendLine(Utility.Text.Format("\t{0} = {1},", uiItem.Value, uiItem.Key));
    //        }
    //        else
    //        {
    //            sBuilder.AppendLine(Utility.Text.Format("\t{0} = {1}", uiItem.Value, uiItem.Key));
    //        }
    //        curIndex++;
    //    }
    //    sBuilder.AppendLine("}");
    //    File.WriteAllText(ConstEditor.UIViewScriptFile, sBuilder.ToString());
    //    Debug.LogFormat("-------------------成功生成UIViews.cs-----------------");
    //}


    /// <summary>
    /// 批量替换字体文件
    /// </summary>
    public static void ReplaceFont()
    {
        EditorUtility.DisplayProgressBar("Progress", "Replace Font...", 0);
        var asstIds = AssetDatabase.FindAssets("t:Prefab", ConstEditor.PrefabsPath);
        int count = 0;
        for (int i = 0; i < asstIds.Length; i++)
        {
            bool isChanged = false;
            string path = AssetDatabase.GUIDToAssetPath(asstIds[i]);
            var pfb = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            //var pfb = PrefabUtility.InstantiatePrefab(pfbFile) as GameObject;//不涉及增删节点,不用实例化
            if (toFont != null)
            {
                var texts = pfb.GetComponentsInChildren<Text>(true);
                foreach (var item in texts)
                {
                    item.font = toFont;
                }
                isChanged = texts.Length > 0;
            }
            if (fontSpAsset != null)
            {
                var tmTexts = pfb.GetComponentsInChildren<TextMeshPro>(true);
                foreach (var item in tmTexts)
                {
                    //item.font = tmFont;
                    if (item.spriteAsset != null && item.spriteAsset.name == "OtherIcons")
                    {
                        item.spriteAsset = fontSpAsset;
                        isChanged = true;
                    }

                }
            }

            if (isChanged)
            {
                PrefabUtility.SavePrefabAsset(pfb, out bool success);
                if (success)
                {
                    count++;
                }
            }
            else
            {
                count++;
            }

            EditorUtility.DisplayProgressBar("Replace Font Progress", pfb.name, count / (float)asstIds.Length);
        }
        EditorUtility.ClearProgressBar();
    }

    #region 通用方法
    public static void FindChildByName(Transform root, string name, ref Transform result)
    {
        if (root.name.StartsWith(name))
        {
            result = root;
            return;
        }

        foreach (Transform child in root)
        {
            FindChildByName(child, name, ref result);
        }
    }
    public static void FindChildrenByName(Transform root, string name, ref List<Transform> result)
    {
        if (root.name.StartsWith(name))
        {
            result.Add(root);

        }

        foreach (Transform child in root)
        {
            FindChildrenByName(child, name, ref result);
        }
    }
    public static string GetNodePath(Transform node, Transform root = null)
    {
        if (node == null)
        {
            return string.Empty;
        }
        Transform curNode = node;
        string path = curNode.name;
        while (curNode.parent != root)
        {
            curNode = curNode.parent;
            path = string.Format("{0}/{1}", curNode.name, path);
        }
        return path;
    }


    /// <summary>
    /// Excel转换为Txt
    /// </summary>
    //public static bool Excel2TxtFile(string excelFileName, string outTxtFile)
    //{
    //    bool result = false;
    //    var fileInfo = new FileInfo(excelFileName);
    //    string tmpExcelFile = UtilityBuiltin.ResPath.GetCombinePath(fileInfo.Directory.FullName, Utility.Text.Format("{0}.temp", fileInfo.Name));
    //    Debug.Log($">>>>>>>>Excel2Txt: excel:{excelFileName}, outTxtFile:{outTxtFile}");
    //    File.Copy(excelFileName, tmpExcelFile, true);

    //    using (var excelPackage = new ExcelPackage(tmpExcelFile))
    //    {
    //        var excelSheet = excelPackage.Workbook.Worksheets[0];
    //        string excelTxt = string.Empty;
    //        for (int rowIndex = excelSheet.Dimension.Start.Row; rowIndex <= excelSheet.Dimension.End.Row; rowIndex++)
    //        {
    //            string rowTxt = string.Empty;
    //            for (int colIndex = excelSheet.Dimension.Start.Column; colIndex <= excelSheet.Dimension.End.Column; colIndex++)
    //            {
    //                rowTxt = Utility.Text.Format("{0}{1}\t", rowTxt, excelSheet.GetValue(rowIndex, colIndex));
    //            }
    //            rowTxt = rowTxt.Substring(0, rowTxt.Length - 1);
    //            excelTxt = Utility.Text.Format("{0}{1}\n", excelTxt, rowTxt);
    //        }
    //        excelTxt = excelTxt.TrimEnd('\n');
    //        excelSheet.Dispose();
    //        excelPackage.Dispose();
    //        try
    //        {
    //            File.WriteAllText(outTxtFile, excelTxt, Encoding.UTF8);
    //            result = true;
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //        }

    //    }
    //    if (File.Exists(tmpExcelFile))
    //    {
    //        File.Delete(tmpExcelFile);
    //    }
    //    return result;
    //}
    //[MenuItem("Game Framework/GameTools/Refresh All GameConfigs")]
    //public static void RefreshAllConfig(string[] files = null)
    //{
    //    var configDir = ConstEditor.ConfigExcelPath;
    //    if (!Directory.Exists(configDir))
    //    {
    //        return;
    //    }
    //    string[] excelFiles;
    //    if (files == null)
    //    {
    //        excelFiles = Directory.GetFiles(configDir, "*.xlsx", SearchOption.TopDirectoryOnly);
    //    }
    //    else
    //    {
    //        excelFiles = GetABTestExcelFiles(configDir, files);
    //    }
    //    int totalExcelCount = excelFiles.Length;
    //    for (int i = 0; i < totalExcelCount; i++)
    //    {
    //        var excelFileName = excelFiles[i];
    //        string savePath = UtilityBuiltin.ResPath.GetCombinePath(ConstEditor.GameConfigPath, Utility.Text.Format("{0}.txt", Path.GetFileNameWithoutExtension(excelFileName)));
    //        EditorUtility.DisplayProgressBar($"{i}/{totalExcelCount}", $"{excelFileName} -> {savePath}", i / (float)totalExcelCount);
    //        try
    //        {
    //            if (Excel2TxtFile(excelFileName, savePath))
    //            {
    //                Debug.LogFormat("------------导出Config表成功:{0}", savePath);
    //            }
    //        }
    //        catch (System.Exception e)
    //        {
    //            Debug.LogErrorFormat("Config数据表转txt失败:{0}", e.Message);
    //            throw;
    //        }
    //    }
    //    EditorUtility.ClearProgressBar();
    //    AssetDatabase.Refresh();
    //}
    //public static async void RefreshAllDataTable(string[] files = null)
    //{
    //    var excelDir = ConstEditor.DataTableExcelPath;
    //    if (!Directory.Exists(excelDir))
    //    {
    //        Debug.LogWarningFormat("Excel数据表文件夹不存在:{0}", excelDir);
    //        return;
    //    }

    //    string[] excelFiles;
    //    if (files == null)
    //    {
    //        excelFiles = Directory.GetFiles(excelDir, "*.xlsx", SearchOption.TopDirectoryOnly);
    //    }
    //    else
    //    {
    //        excelFiles = GetABTestExcelFiles(excelDir, files);
    //    }
    //    int totalExcelCount = excelFiles.Length;
    //    for (int i = 0; i < totalExcelCount; i++)
    //    {
    //        var excelFileName = excelFiles[i];
    //        var fileName = Path.GetFileNameWithoutExtension(excelFileName);
    //        string savePath = UtilityBuiltin.ResPath.GetCombinePath(ConstEditor.DataTablePath, Utility.Text.Format("{0}.txt", fileName));
    //        EditorUtility.DisplayProgressBar($"Excel -> txt: ({i}/{totalExcelCount})", $"{excelFileName} -> {savePath}", i / (float)totalExcelCount);
    //        try
    //        {
    //            if (Excel2TxtFile(excelFileName, savePath))
    //            {
    //                Debug.Log($"------------Excel -> txt:{excelFileName} -> {savePath}");
    //            }
    //        }
    //        catch (System.Exception e)
    //        {
    //            Debug.LogErrorFormat("Excel数据表转txt失败:{0}", e.Message);
    //            throw;
    //        }

    //    }
    //    EditorUtility.ClearProgressBar();
    //    //生成数据表代码
    //    var appConfig = await AppConfigs.GetInstanceSync();
    //    int dataTbCount = appConfig.DataTables.Length;
    //    for (int i = 0; i < dataTbCount; i++)
    //    {
    //        var dataTableName = appConfig.DataTables[i];
    //        string tbTxtFile = Utility.Path.GetRegularPath(Path.Combine(ConstEditor.DataTablePath, dataTableName + ".txt"));
    //        EditorUtility.DisplayProgressBar($"进度:({i}/{dataTbCount})", $"生成代码:{dataTableName}", i / (float)dataTbCount);
    //        if (!File.Exists(tbTxtFile))
    //        {
    //            continue;
    //        }
    //        DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
    //        if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
    //        {
    //            Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
    //            break;
    //        }

    //        DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
    //    }
    //    EditorUtility.ClearProgressBar();
    //    AssetDatabase.Refresh();
    //}
    private static string[] GetABTestExcelFiles(string excelDir, string[] files)
    {
        string[] excelFiles = Directory.GetFiles(excelDir, "*.xlsx", SearchOption.TopDirectoryOnly);
        string[] result = new string[0];
        foreach (var rawName in files)
        {
            string abFileHeader = rawName + "_";
            foreach (var excelFile in excelFiles)
            {
                string excelName = Path.GetFileNameWithoutExtension(excelFile);
                bool isABFile = excelName.CompareTo(rawName) == 0 || excelName.StartsWith(abFileHeader);
                if (isABFile) ArrayUtility.Add(ref result, excelFile);
            }
        }
        return result;
    }
    private static UnityEditor.BuildTargetGroup GetCurrentBuildTarget()
    {
#if UNITY_ANDROID
        return UnityEditor.BuildTargetGroup.Android;
#elif UNITY_IOS
        return UnityEditor.BuildTargetGroup.iOS;
#elif UNITY_STANDALONE
        return UnityEditor.BuildTargetGroup.Standalone;
#elif UNITY_WEBGL
        return UnityEditor.BuildTargetGroup.WebGL;
#else
        return UnityEditor.BuildTargetGroup.Unknown;
#endif
    }
#if UNITY_2021_1_OR_NEWER
    private static UnityEditor.Build.NamedBuildTarget GetCurrentNamedBuildTarget()
    {
#if UNITY_ANDROID
        return UnityEditor.Build.NamedBuildTarget.Android;
#elif UNITY_IOS
        return UnityEditor.Build.NamedBuildTarget.iOS;
#elif UNITY_STANDALONE
        return UnityEditor.Build.NamedBuildTarget.Standalone;
#elif UNITY_WEBGL
        return UnityEditor.Build.NamedBuildTarget.WebGL;
#else
        return UnityEditor.Build.NamedBuildTarget.Unknown;
#endif
    }
#endif
    #endregion
}
#endif