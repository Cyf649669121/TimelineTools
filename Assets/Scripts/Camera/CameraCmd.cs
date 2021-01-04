using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机控制命令；
    /// 
    /// ——程一峰；2020.10.31
    /// </summary>
    [System.Serializable]
    public struct CameraCmd
    {
        /// <summary>
        /// 空信息；
        /// </summary>
        public static CameraCmd Null = new CameraCmd() { };

        public bool IsNull { get { return CmdType == E_CameraCmd.None; } }

        /// <summary>
        /// 指令来源
        /// </summary>
        public string FromGameObject;

        /// <summary>
        /// 相机的移动类型；
        /// </summary>
        public E_CameraCmd CmdType;

        /// <summary>
        /// 目标位置；
        /// </summary>
        public Vector3 TargetPos;

        /// <summary>
        /// 目标旋转；
        /// </summary>
        public Quaternion TargetRot;

        /// <summary>
        /// 命令优先级；
        /// 低优先级的命令不能覆盖高优先级的命令；
        /// </summary>
        public int Order;

        /// <summary>
        /// 尝试融合两个命令；
        /// </summary>
        /// <param name="aCmd"></param>
        /// <returns>返回是否成功融合</returns>
        public bool Do_TryMixCmd(CameraCmd aCmd)
        {
            //1、先判定能不能融合；只要两个命令有一个可以融合即可；
            if (CmdType != E_CameraCmd.Additional && aCmd.CmdType != E_CameraCmd.Additional)
                return false;

            //2、融合，这里只做简单加和；
            TargetPos += aCmd.TargetPos;
            TargetRot.eulerAngles += aCmd.TargetRot.eulerAngles;

            return true;
        }

        public override string ToString() { return $"{FromGameObject } : [{CmdType}] Pos : {TargetPos} ; Rot : {TargetRot} ; Order : {Order}"; }

    }
}