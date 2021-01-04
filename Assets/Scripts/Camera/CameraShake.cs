using UnityEngine;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机抖动类；
    /// 
    /// ——改造中，可能还不好用；
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        #region 主相机上的静态引用；

        /*
         * 先改造一部分；
         * 
         * 把这个 CameraShake 挂载在 Camera.Main上；
         * 如要抖动就只需要抖动本身即可；
         * 不用每次都去找一次 Camera.Main;
         * 
         * ——程一峰；2020.10.30
         */

        private static CameraShake _Get;
        public static CameraShake Get
        {
            get
            {
                if (_Get == null)
                {
                    var mainCameraObj = new GameObject("CameraShake");
                    _Get = mainCameraObj.AddComponent<CameraShake>();
                    mainCameraObj.transform.SetParent(CameraRoot.Instance.transform);
                    //添加一个自动发送的相机脚本；
                    var cmdSender = mainCameraObj.AddComponent<CameraCmdSender_Auto>();
                    cmdSender.Order = 1;
                    cmdSender.cmdType = E_CameraCmd.Additional;
                    cmdSender.IsLockCamera = true;
                }
                return _Get;
            }
        }

        private void Start() { gameObject.SetActive(false); }

        private void Awake()
        {
            _Get = this;
        }

        #endregion

        public float shakeCD = 0.01f;                //抖动的频率
        public int shakeCount = 15;              //设置抖动次数
        public float shakeDegree = 2f;
        public float fadeRate = 1f;//衰减度
        public Vector3 axis = new Vector3(2, 2, 0);
        public int mulityCount = 1;//重复抖动次数
        public float mulityDelay = 0;

        private int currentCount;
        private float currentShakeDegree;
        private bool m_isRulerEnd;
        public System.Action m_a_onComplete;

        public void ShowShake(CameraShakeConfig config, bool isRulerEnd = true)
        {
            gameObject.SetActive(true);

            shakeCD = config.shakeCD;
            shakeCount = config.shakeCount;
            shakeDegree = config.shakeDegree;
            fadeRate = config.fadeRate;
            axis = config.axis;
            m_isRulerEnd = isRulerEnd;
            mulityCount = config.mulityCount;
            mulityDelay = config.mulityDelay;
            if (mulityCount > 1)
                InvokeRepeating("DoRepeatShake", 0, mulityDelay);
            else
                ShowShake();
        }

        private void ShowShake()
        {
            currentShakeDegree = shakeDegree;
            if (currentCount > 0)
            {
                currentCount = 0;
                CancelInvoke("DoShake");
            }
            InvokeRepeating("DoShake", 0, shakeCD);
        }

        public bool IsPlaying { get => currentCount > 0; }

        public void Stop()
        {
            currentCount = 0;
            CancelInvoke("DoRepeatShake");
            CancelInvoke("DoShake");
            m_a_onComplete?.Invoke();

            gameObject.SetActive(false);
        }

        private void DoShake()
        {
            if (currentCount < shakeCount)
            {
                //transform.position = CameraTool.Instance.LatePosition;

                currentCount++;
                float radiox = Random.Range(-currentShakeDegree, currentShakeDegree) * axis.x;
                float radioy = Random.Range(-currentShakeDegree, currentShakeDegree) * axis.y;
                float radioz = Random.Range(-currentShakeDegree, currentShakeDegree) * axis.z;

                if (currentCount >= shakeCount)    //抖动最后一次时设置为抖动前记录的位置
                {
                    radiox = 0;
                    radioy = 0;
                    radioz = 0;
                    currentCount = 0;
                    CancelInvoke("DoShake");
                    m_a_onComplete?.Invoke();
                }

                currentShakeDegree *= fadeRate;
                if (m_isRulerEnd)
                {
                    /*
                     * 抖动只需要原地抽搐即可；
                     * 保持自己和相机在同一位置；
                     * 如果不需要抖动了，就关闭这个物体；
                     * 
                     * ——程一峰；2020.10.31
                    */
                    transform.position = new Vector3(radiox, radioy, radioz);
                }
                else
                    transform.position += new Vector3(radiox, radioy, radioz);
            }
        }

        private void DoRepeatShake()
        {
            if (mulityCount > 0)
                ShowShake();
            else
                Stop();
            mulityCount--;
        }

    }
}