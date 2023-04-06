#if UNITY_EDITOR
using System.IO;
using UnityEngine;
/// <summary>
/// 默认编辑器配置项
/// </summary>
public class ConstEditor
{
    public const bool AutoScriptUTF8 = true;//新建脚本时自动修改脚本编码方式为utf-8以支持中文
    public const string UIViewScriptFile = "Assets/AAAGame/Scripts/UI/UIViews.cs";
    public const string UISerializeFieldDir = "Assets/AAAGame/Scripts/UI/UIVariables";//生成UI变量代码目录
    public const string UITableExcel = "UITable.xlsx";
    public const string EntityGroupTableExcel = "EntityGroupTable.xlsx";
    public const string SoundGroupTableExcel = "SoundGroupTable.xlsx";
    public const string UIGroupTableExcel = "UIGroupTable.xlsx";
    public const string ConstGroupScriptFileFullName = "Assets/AAAGame/Scripts/Common/Const.Groups.cs";

    public static readonly string[] PrefabsPath = { "Assets/AAAGame/Prefabs/" };
    public static readonly string[] ScenePath = { "Assets/AAAGame/Scene/" };

    public const string DataTableCodeTemplate = "Assets/AAAGame/ScriptsBuiltin/Editor/DataTableGenerator/DataTableCodeTemplate/DataTableCodeTemplate.txt"; //生成配置表代码的模板文件
    public const string BuiltinAssembly = "Assets/AAAGame/ScriptsBuiltin/Runtime/Builtin.Runtime.asmdef";
    public const string HotfixAssembly = "Assets/AAAGame/Scripts/Hotfix.asmdef";


    public const string SharedAssetBundleName = "SharedAssets";//AssetBundle分包共用资源
    public static readonly string[] DefaultLayers = { "UI", "WorldUI" };
    internal static readonly string KeystorePass = "topgames";
    internal static readonly string KeyAliasPass = "topgames";
    internal static string KeystoreName => UtilityBuiltin.ResPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "user.keystore");
    internal static readonly string KeyAliasName = "release";
    internal static string AssetBundleOutputPath => UtilityBuiltin.ResPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AB");
    public static readonly string UpdatePrefixUri = "https://gitcode.net/topgamesopen/gf_hybridclr_hotfix/-/raw/master";//默认资源下载地址
    internal static readonly string AppUpdateUrl = "https://play.google.com/store/apps/details?id=";

    /// <summary>
    /// 数据表Excel目录
    /// </summary>
    public static string DataTableExcelPath => UtilityBuiltin.ResPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAAGameData/DataTables");
    /// <summary>
    /// 配置表Excel目录
    /// </summary>
    public static string ConfigExcelPath => UtilityBuiltin.ResPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAAGameData/Configs");
    /// <summary>
    /// 语言国际化Excel目录
    /// </summary>
    public static string LanguageExcelPath => UtilityBuiltin.ResPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAAGameData/Languages");

    public const string DataTablePath = "Assets/AAAGame/DataTable";
    public const string GameConfigPath = "Assets/AAAGame/Config";
    public const string DataTableCodePath = "Assets/AAAGame/Scripts/DataTable";
}
#endif