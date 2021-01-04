using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机各个控制器的根节点；
    /// </summary>
    public class CameraRoot : MonoSingleton<CameraRoot>
    {
        /*
         * 各个控制相机的控件都放在这个节点下面；
         * 主相机也放在这个节点下面；
         * 方便在Scene视图中查询；
         * 
         * 这个类本身不打算有任何功能；
         * 
         * ——程一峰；2020.10.31
        */

        private void Start()
        {
            //把主相机移动到自己下面；
            transform.parent = null;
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            var mainCamera = CameraTool.Instance;
            if (mainCamera)
                mainCamera.transform.SetParent(transform);
        }

        public void Create() { Debug.Log($"CameraRoot Create name:{name}"); }

    }
}