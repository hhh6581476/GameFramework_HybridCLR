//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
using System;
using UnityEngine;
namespace StarForce
{
    [Serializable]
    public class BuildInfo
    {
        public string GameVersion;

        public int InternalGameVersion;

        public string CheckVersionUrl;
        public string WindowsAppUrl;

        public string MacOSAppUrl;

        public string IOSAppUrl;

        public string AndroidAppUrl;
        public string END_OF_JSON;
    }
}
