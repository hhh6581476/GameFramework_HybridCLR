//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace StarForce
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,


        HomeForm = 103,

        MineForm = 104,
        /// <summary>
        /// 创作预览界面
        /// </summary>
        CreatePreviewForm = 105,

        /// <summary>
        /// 创作编辑界面
        /// </summary>
        CreateViewForm = 106,

        /// <summary>
        /// 创作编辑文字界面
        /// </summary>
        CreateEditTextForm = 107,

        /// <summary>
        ///创作编辑语音讲解界面 
        /// </summary>
        CreateEditAudioForm = 108,
        /// <summary>
        /// 登录界面
        /// </summary>
        LoginForm =200,
    }

    public enum UIGroupId : byte
    {
        Undefined = 0,
        Default = 1
    }
}
