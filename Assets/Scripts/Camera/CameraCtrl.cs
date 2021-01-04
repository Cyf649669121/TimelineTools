
using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机控制；
    /// 真正可以操作相机的类；
    /// </summary>
    public class CameraCtrl : MonoBehaviour
    {

        /*
         * 说明一：
         *      相机的控制之前是直接调用的Camera.Main来进行控制，其实风险挺大的；
         *      如果是一个人写脚本还好，如果很多人协同，如果出了问题，甚至Log都不好打；
         *      
         *      除此之外，还有Animator、DoTween、Cinemation都会控制相机。
         *      如果因为某个插件的操作失误导致相机锁死，甚至很难确定相机是在哪个插件/脚本的控制中。
         *      
         *      所以在多个插件、脚本都需要控制相机时，采用这个类做最后的收尾；
         *      其他的所有类和插件都不能直接控制相机，只能把需要传递的命令给 CameraCtrl ；
         *      然后 CameraCtrl 再做最后的收尾工作。
         *      至少我们就能知道相机当前是处在谁的控制之下。
         *      
         * 说明二：
         *      这个类的功能只是接受各种命令，然后处理命令；
         *      每帧命令处理完成之后就清空命令；
         *      目前的命令只有设置位置和方向，也就是说需要每帧都获取命令（否则就不动）；
         *      其他的一些命令可以放到 CameraTool 中 ；
         * 
         * ——程一峰；2020.10.31
        */

        #region 实例获取

        private static CameraCtrl _Instance;
        public static CameraCtrl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var mainCamera = CameraTool.Instance;
                    if (mainCamera)
                        _Instance = mainCamera.gameObject.AddComponent<CameraCtrl>();
                    else
                        Debug.LogError("Camera.main Is NULL !!!");
                }
                return _Instance;
            }
            set { _Instance = value; }
        }

        private void Awake() { Instance = this; }

        #endregion;

        /*
         * 锁定命令和叠加命令：
         * 
         * 锁定命令是会按照优先级覆盖；
         * 叠加命令目前的算法是都加和进来（相对位移）；
         * 
         * 最后执行的时候，把锁定命令和叠加命令进行相加；
         * 作为最终命令；
         * 
         * ——程一峰；2020.11.07
         * 
        */

        /// <summary>
        /// 锁定命令；
        /// </summary>
        private CameraCmd m_LockCmd = CameraCmd.Null;

        /// <summary>
        /// 叠加命令；
        /// </summary>
        private CameraCmd m_AdditonalCmd = CameraCmd.Null;

        /// <summary>
        /// 执行命令；
        /// </summary>
        /// <param name="aCmd"></param>
        /// <returns></returns>
        public bool Do_HandleCmd(CameraCmd aCmd)
        {
            //判空；
            if (aCmd.IsNull)
                return false;

            //融合命令，走融合命令专门逻辑；
            if (aCmd.CmdType == E_CameraCmd.Additional)
                return Do_HandleCmd_Additional(aCmd);

            //锁定位移和旋转的命令；
            if (aCmd.CmdType == E_CameraCmd.LockPosAndRot)
                return Do_HandleCmd_Lock(aCmd);

            return false;
        }

        /// <summary>
        /// 处理融合命令；
        /// </summary>
        /// <param name="aCmd"></param>
        /// <returns></returns>
        private bool Do_HandleCmd_Additional(CameraCmd aCmd)
        {
            if (m_AdditonalCmd.IsNull)
                m_AdditonalCmd = aCmd;
            else
                m_AdditonalCmd.Do_TryMixCmd(aCmd);

            /*
             * 如果有叠加命令，但是没有锁定命令。
             * 需要自动生成一个 LockPosAndRot 命令用来原地叠加。
             * 
             * ——程一峰；2020.11.07
            */

            if (m_LockCmd.IsNull)
            {
                m_LockCmd = new CameraCmd()
                {
                    CmdType = E_CameraCmd.LockPosAndRot,
                    Order = -1,
                    TargetPos = transform.position,
                    TargetRot = transform.rotation,
                };
            }
            return true;
        }

        private bool Do_HandleCmd_Lock(CameraCmd aCmd)
        {
            //比对当前命令的优先级；
            if (m_LockCmd.Order > aCmd.Order)
                return false;

            m_LockCmd = aCmd;
            return true;
        }

        private void LateUpdate()
        {
            if (m_LockCmd.IsNull)
                return;

            //融合锁定命令和叠加命令；
            m_LockCmd.Do_TryMixCmd(m_AdditonalCmd);

#if UNITY_EDITOR
            lastCmd = m_LockCmd;
#endif

            transform.position = m_LockCmd.TargetPos;
            transform.rotation = m_LockCmd.TargetRot;

            m_LockCmd = CameraCmd.Null;
            m_AdditonalCmd = CameraCmd.Null;
        }

#if UNITY_EDITOR

        public CameraCmd lastCmd;

#endif

    }
}