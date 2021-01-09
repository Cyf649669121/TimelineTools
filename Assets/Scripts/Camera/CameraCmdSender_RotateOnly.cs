/*
 *Copyright(C) 2020 by  GYYX All rights reserved.
 *Unity版本：2018.4.23f1 
 *作者:程一峰  
 *创建日期: 2021-01-08 
 *模块说明：
 *版本: 1.51544
*/


using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 只有角度旋转的叠加；
    /// </summary>
    public class CameraCmdSender_RotateOnly : CameraCmdSender_Auto
    {

        protected override Vector3 GetPosition { get { return Vector3.zero; } }

        protected override Quaternion GetRotation
        {
            get
            {
                Vector3 angle = transform.eulerAngles;
                return Quaternion.Euler(0, 0, angle.z);
            }
        }

    }
}
