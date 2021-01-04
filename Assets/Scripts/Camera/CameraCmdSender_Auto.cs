using UnityEngine;

namespace Warfare.KWarCamera
{
    public class CameraCmdSender_Auto : MonoBehaviour
    {

        /// <summary>
        /// 当前的相机；
        /// </summary>
        public CameraCtrl cameraCtrl;

        /// <summary>
        /// 命令类型；
        /// </summary>
        public E_CameraCmd cmdType;

        /// <summary>
        /// 优先级；
        /// </summary>
        public int Order;

        private string ObjectName;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (cameraCtrl == null)
                cameraCtrl = CameraCtrl.Instance;

            ObjectName = gameObject.name;
        }

        private Vector3 mLastPosition;
        private Quaternion mLastRotation;

        /// <summary>
        /// 锁定相机与自己相同；
        /// </summary>
        public bool IsLockCamera = true;

        protected virtual void Update()
        {
            Vector3 curPos = GetPosition;
            Quaternion curRot = GetRotation;

            if (curPos != mLastPosition || curRot != mLastRotation || IsLockCamera)
            {
                mLastPosition = curPos;
                mLastRotation = curRot;

                //发送位置改变的消息给相机；
                CameraCmd cmd = new CameraCmd()
                {
                    TargetPos = curPos,
                    TargetRot = curRot,
                    CmdType = this.cmdType,
                    Order = Order,
                    FromGameObject = ObjectName,
                };
                cameraCtrl?.Do_HandleCmd(cmd);
            }
        }

        /// <summary>
        /// 获取位置；
        /// </summary>
        protected virtual Vector3 GetPosition
        { get { return transform.position; } }

        /// <summary>
        /// 获取旋转；
        /// </summary>
        protected virtual Quaternion GetRotation
        { get { return transform.rotation; } }

    }
}