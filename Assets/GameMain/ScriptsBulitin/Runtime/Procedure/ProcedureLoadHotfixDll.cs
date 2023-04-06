using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using System.Linq;
using HybridCLR;
using System.Collections.Generic;

namespace StarForce
{
    public class ProcedureLoadHotfixDll : ProcedureBase
    {
        /// <summary>
        /// 全部的预加载热更脚本dll
        /// </summary>
        private string[] hotfixDlls;
        private bool hotfixListIsLoaded;
        private int totalProgress;
        private int loadedProgress;

        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 使用BetterStreamingAssets插件，即使在Android平台也可以直接读取StreamingAssets下内容，简化演示。
            BetterStreamingAssets.Initialize();

#if !DISABLE_HYBRIDCLR
            GameEntry.Event.Subscribe(LoadHotfixDllEventArgs.EventId, OnLoadHotfixDllCallback);
#endif
            PreloadAndInitData();
        }


        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
#if !DISABLE_HYBRIDCLR
            GameEntry.Event.Unsubscribe(LoadHotfixDllEventArgs.EventId, OnLoadHotfixDllCallback);
#endif
            base.OnLeave(procedureOwner, isShutdown);
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!hotfixListIsLoaded)
            {
                return;
            }
            //加载热更新Dll完成,进入热更逻辑
            if (loadedProgress >= totalProgress)
            {
                loadedProgress = -1;
                var entryFunc = Utility.Assembly.GetType("StarForce.HotfixEntry")?.GetMethod("StartHotfixLogic", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (entryFunc == null)
                {
                    Log.Error("游戏启动失败, 未找到HotfixEntry.StartHotfixLogic入口函数");
                    return;
                }

                //ChangeState<ProcedurePreload>(procedureOwner);

#if !DISABLE_HYBRIDCLR
                entryFunc?.Invoke(null, new object[] { true });
#else
            entryFunc?.Invoke(null, new object[] { false });
#endif
            }
        }

        /// <summary>
        /// 加载热更新dll
        /// </summary>
        private void PreloadAndInitData()
        {
            //显示进度条
            //GameEntry.BuiltinView.ShowLoadingProgress();
            totalProgress = 0;
            loadedProgress = 0;
            if (GameEntry.Base.EditorResourceMode)
            {
                // 编辑器模式
                hotfixListIsLoaded = true;
            }
            else
            {
                // 可更新模式
                hotfixListIsLoaded = false;
                LoadMetadataForAOTAssemblies();
                LoadHotfixDlls();
            }
        }
#if !DISABLE_HYBRIDCLR


        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        private static void LoadMetadataForAOTAssemblies()
        {
            List<string> aotMetaAssemblyFiles = new List<string>()
        {
           ConstBuiltin.AOT_DLL_DIR+"/"+"mscorlib.dll",
            ConstBuiltin.AOT_DLL_DIR+"/"+"System.dll",
            ConstBuiltin.AOT_DLL_DIR+"/"+"System.Core.dll",
        };
            /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
            /// 
            HomologousImageMode mode = HomologousImageMode.SuperSet;
            foreach (var aotDllName in aotMetaAssemblyFiles)
            {
                byte[] dllBytes = BetterStreamingAssets.ReadAllBytes(aotDllName + ".bytes");
                // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
                Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
            }
        }


        /// <summary>
        /// 补充元数据
        /// </summary>
        private void LoadAotDlls()
        {
            var aotMetaDlls = Resources.LoadAll<TextAsset>(ConstBuiltin.AOT_DLL_DIR);
            totalProgress += aotMetaDlls.Length;
            LoadMetadata(aotMetaDlls);
        }
        private void LoadMetadata(TextAsset[] aotMetaDlls)
        {
            foreach (var dll in aotMetaDlls)
            {
                var success = GameEntry.Hotfix.LoadMetadataForAOTAssembly(dll.bytes);
                Log.Info(Utility.Text.Format("补充元数据:{0}. ret:{1}", dll.name, success));
                if (success)
                {
                    loadedProgress++;
                }
            }
        }
        private void LoadHotfixDlls()
        {
            Log.Info("开始加载热更新dll");
            var hotfixListFile = UtilityBuiltin.ResPath.GetCombinePath("Assets", ConstBuiltin.HOT_FIX_DLL_DIR, "HotfixFileList.txt");
            if (GameEntry.Resource.HasAsset(hotfixListFile) == GameFramework.Resource.HasAssetResult.NotExist)
            {
                Log.Fatal("热更新dll列表文件不存在:{0}", hotfixListFile);
                return;
            }
            GameEntry.Resource.LoadAsset(hotfixListFile, new GameFramework.Resource.LoadAssetCallbacks((string assetName, object asset, float duration, object userData) =>
            {
                var textAsset = asset as TextAsset;
                if (textAsset != null)
                {
                    hotfixDlls = UtilityBuiltin.Json.ToObject<string[]>(textAsset.text);
                    totalProgress += hotfixDlls.Length;
                    for (int i = 0; i < hotfixDlls.Length; i++)
                    {
                        var dllName = hotfixDlls[i];
                        var dllAsset = UtilityBuiltin.ResPath.GetHotfixDll(dllName);
                        GameEntry.Hotfix.LoadHotfixDll(dllAsset, this);
                    }
                    hotfixListIsLoaded = true;
                }
            }));
        }


        private void OnLoadHotfixDllCallback(object sender, GameEventArgs e)
        {
            var args = e as LoadHotfixDllEventArgs;
            if (args.UserData != this)
            {
                return;
            }
            if (args.Assembly == null)
            {
                Log.Error("加载dll失败:{0}", args.DllName);
                return;
            }

            loadedProgress++;
            //GameEntry.BuiltinView.SetLoadingProgress(loadedProgress / totalProgress);

            //所有依赖dll加载完成后再加载Hotfix.dll
            if (loadedProgress + 1 == totalProgress)
            {
                var mainDll = UtilityBuiltin.ResPath.GetHotfixDll(hotfixDlls.Last());
                GameEntry.Hotfix.LoadHotfixDll(mainDll, this);
            }
        }
#endif
    }
}
