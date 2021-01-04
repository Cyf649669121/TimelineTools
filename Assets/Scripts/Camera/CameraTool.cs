using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机工具；
    /// </summary>
    public class CameraTool : MonoBehaviour
    {

        /*
         * 用这个方法来获取主相机的一些属性，如位置、旋转等；
         * 不要直接用Camera.Main ;
         * 
         *首先，Camera.Main 这个函数的性能并不好；
         *其次，如果都对 Camera.Main 进行读写，逻辑上风险很大；
         *
         *——程一峰；2020.10.30
         */

        #region 实例获取

        public static CameraTool Instance { get; private set; }

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
            Instance = this;
            Debug.Log("CameraTool Awake");
        }

        #endregion;


        private static Camera _mainCamera;
        public static Camera mainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Instance?.GetComponent<Camera>();

                if (_mainCamera == null)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }

        public static bool IsEnable
        {
            get
            {
                if (_mainCamera == null)
                    return false;

                if (!_mainCamera.enabled)
                    return false;

                if (!_mainCamera.gameObject.activeSelf)
                    return false;

                if (!_mainCamera.gameObject.activeInHierarchy)
                    return false;

                return true;
            }
        }

        public int cullingMask
        {
            get
            {
                if (mainCamera == null)
                    return -1;
                return mainCamera.cullingMask;
            }

            set
            {
                if (mainCamera)
                    mainCamera.cullingMask = value;
            }
        }

        #region fieldOfView

        public void SetFieldOfView(float val) { mainCamera.fieldOfView = val; }

        public float fieldOfView => mainCamera.fieldOfView;

        public float aspect => mainCamera.aspect;

        /// <summary>
        /// 设置进远裁剪面；
        /// </summary>
        /// <param name="near"></param>
        /// <param name="far"></param>
        public void Set_NearFarClipPlane(float near, float far)
        {
            mainCamera.nearClipPlane = near;
            mainCamera.farClipPlane = far;
        }

        #endregion

        #region 背景部分；

        /// <summary>
        /// 设置为背景；
        /// </summary>
        public void Do_SetAsBalckBackground()
        {
            mainCamera.clearFlags = CameraClearFlags.Color;
            mainCamera.backgroundColor = Color.black;
        }

        /// <summary>
        /// 设置为天空盒渲染
        /// </summary>
        public void Do_SetAsSkyBox()
        {
            mainCamera.clearFlags = CameraClearFlags.Skybox;
            mainCamera.backgroundColor = Color.blue;
        }

        #endregion

        private void OnDestroy() { Debug.LogWarning("主相机已被卸载！"); }

    }
}