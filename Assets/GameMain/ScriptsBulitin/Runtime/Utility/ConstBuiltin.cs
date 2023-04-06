﻿
using System.Collections.Generic;

/// <summary>
/// 内置Const(非热更)
/// </summary>
public static class ConstBuiltin
{
    public static readonly string HOT_FIX_DLL_DIR = "GameMain/HotfixDlls";
    public static readonly string AOT_DLL_DIR = "AotDlls";//相对于Resources目录
    public static readonly string CheckVersionUrl = "https://gitcode.net/topgamesopen/gf_hybridclr_hotfix/-/raw/master";//热更新检测地址
    public static readonly string VersionFile = "version.json";
    public static readonly bool NoNetworkAllow = true;//热更模式时没网络是否允许进入游戏
    internal const string DES_KEY = "VaBwUXzd";//网络数据DES加密
    public static readonly string RES_KEY = "password";//AB包加密解密key


    /// <summary>
    /// 用户设置Key
    /// </summary>
    public static class Setting
    {
        /// <summary>
        /// 语言国际化
        /// </summary>
        public static readonly string Language = "Setting.Language";
        /// <summary>
        /// 退出App时间
        /// </summary>
        public static readonly string QuitAppTime = "Setting.QuitAppTime";
        /// <summary>
        /// A/B测试组
        /// </summary>
        public static readonly string ABTestGroup = "Setting.ABTestGroup";
    }
}
