/*
 *Copyright(C) 2020 by  GYYX All rights reserved.
 *Unity版本：2018.4.23f1 
 *作者:程一峰  
 *创建日期: 2020-11-23 
 *模块说明：
 *版本: 1.2
*/

using Cinemachine;
using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 自动相机位置发射器：
    /// 针对CinemationBrain的改造；
    /// </summary>
    public class CameraCmdSender_CinemationBarin : CameraCmdSender_Auto
    {

        public CinemachineBrain CM_Brain;

        protected override void Start()
        {
            base.Start();
            CM_Brain = GetComponent<CinemachineBrain>();
        }

        protected override Vector3 GetPosition { get { return CM_Brain.CurrentCameraState.CorrectedPosition; } }

        protected override Quaternion GetRotation { get { return CM_Brain.CurrentCameraState.CorrectedOrientation; } }

        protected override void Update()
        {
            base.Update();
            //同时还改FOW等效果；
            float fow = CM_Brain.CurrentCameraState.Lens.FieldOfView;
            CameraTool.Instance.SetFieldOfView(fow);

            float nearClip = CM_Brain.CurrentCameraState.Lens.NearClipPlane;
            float farClip = CM_Brain.CurrentCameraState.Lens.FarClipPlane;
            CameraTool.Instance.Set_NearFarClipPlane(nearClip, farClip);
        }

    }
}
