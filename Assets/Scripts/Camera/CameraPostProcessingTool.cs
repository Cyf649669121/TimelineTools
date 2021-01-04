using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Warfare.KWarCamera
{
    /// <summary>
    /// 相机的后处理小工具；
    /// </summary>
    public class CameraPostProcessingTool : MonoBehaviour
    {
        /*
         * 这个类挂载在PostProcessVolume 来调整相机后处理；
         * 只在场景中只有一个后处理类的时候好用；
         * 
         * 目前只写了一个类型的控制，后续可以添加；
         * 
         * ——程一峰；2020.11.05
        */

        #region 初始化

        private static CameraPostProcessingTool _Instance;

        public static CameraPostProcessingTool Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject go = FindPostProcessObject();
                    if (go != null)
                        _Instance = go.AddComponent<CameraPostProcessingTool>();
                }
                return _Instance;
            }
            private set
            {
                _Instance = value;
            }
        }

        /// <summary>
        /// 找场景中的 PostProcessVolume
        /// </summary>
        /// <returns></returns>
        private static GameObject FindPostProcessObject()
        {
            /*
             * 这个找法并不保险；
             * 因为并不是所有场景都按照规则命名；
             * 
             * ——程一峰；2020.11.05
            */

            GameObject go = GameObject.Find("Scene");
            if (!go)
                return null;

            var post = go.GetComponentInChildren<PostProcessVolume>();
            if (post)
                return post.gameObject;
            return null;
        }

        private void Awake() { Instance = this; }

        public PostProcessVolume postProcessVolume;

        void Start()
        {
            if (!postProcessVolume)
                postProcessVolume = GetComponent<PostProcessVolume>();

            InitChromaticAberration();
        }

        #endregion

        #region ChromaticAberration 部分；

        /// <summary>
        /// 初始化 ChromaticAberration
        /// </summary>
        private void InitChromaticAberration()
        {
            if (postProcessVolume)
            {
                postProcessVolume.profile.TryGetSettings(out chromaticAberration);
                if (chromaticAberration == null)
                {
                    //此时添加一个；
                    chromaticAberration = postProcessVolume.profile.AddSettings<ChromaticAberration>();
                    chromaticAberration.SetAllOverridesTo(true);
                    chromaticAberration.active = true;
                    chromaticAberration.enabled.value = false;
                    chromaticAberration.fastMode.value = false;
                }
            }
        }

        /// <summary>
        /// 色差，边缘模糊的感觉；
        /// </summary>
        private ChromaticAberration chromaticAberration;

        /// <summary>
        /// 默认的一张贴图；可以为空；
        /// </summary>
        private Texture tex_ChormaticAberrationDefaultSpectraLut;

        /// <summary>
        /// 打开ChromaticAberration
        /// </summary>
        /// <param name="intensity"></param>
        /// <param name="spectralLut"></param>
        /// <param name="useDefaultTex">如果 spectralLut 参数为空会采用默认值 </param>
        public void Do_OpenChromaticAberration(float intensity, Texture spectralLut = null, bool useDefaultTex = true)
        {
            if (chromaticAberration == null)
                return;

            chromaticAberration.enabled.value = true;
            chromaticAberration.intensity.value = intensity;
            //设置贴图，如果无，则改回默认值；
            if (spectralLut == null && useDefaultTex)
                spectralLut = tex_ChormaticAberrationDefaultSpectraLut;
            chromaticAberration.spectralLut.value = spectralLut;
        }

        /// <summary>
        /// 关闭ChromaticAberration
        /// </summary>
        public void Do_CloseChromaticAberration()
        {
            if (chromaticAberration == null)
                return;

            chromaticAberration.enabled.value = false;
        }

        /// <summary>
        /// 设置模糊的默认值；
        /// </summary>
        /// <param name="tex"></param>
        public void Do_SetDefaultSepectraLut(Texture tex = null) { tex_ChormaticAberrationDefaultSpectraLut = tex; }

        #endregion

    }
}