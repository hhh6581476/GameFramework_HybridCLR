using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using UnityGameFramework.Runtime;
/// <summary>
/// 热更逻辑入口
/// </summary>
/// 

namespace StarForce
{
    public class HotfixEntry
    {
        public static void StartHotfixLogic(bool enableHotfix)
        {
            Log.Info("Hotfix Enable:{0}", enableHotfix);
            //AwaitExtension.SubscribeEvent();

            /*
            GameEntry.Fsm.DestroyFsm<IProcedureManager>();
            var fsmManager = GameFrameworkEntry.GetModule<IFsmManager>();
            var procManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            string[] mProcedures = { "StarForce.ProcedureChangeScene", "StarForce.ProcedureMain", "StarForce.ProcedureMenu", "StarForce.ProcedurePreload" };
            //var appConfig = await AppConfigs.GetInstanceSync();
            ProcedureBase[] procedures = new ProcedureBase[mProcedures.Length];
            for (int i = 0; i < mProcedures.Length; i++)
            {
                procedures[i] = Activator.CreateInstance(Type.GetType(mProcedures[i])) as ProcedureBase;
            }
            procManager.Initialize(fsmManager, procedures);
            procManager.StartProcedure<ProcedureMenu>();

            */
            GameEntry.UI.OpenUIForm(UIFormId.LoginForm);

        }
    }
}

