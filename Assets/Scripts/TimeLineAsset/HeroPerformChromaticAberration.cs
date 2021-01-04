using UnityEngine;
using UnityEngine.Playables;
using Warfare.KWarCamera;

namespace Warfare.Game.Perform
{
    /// <summary>
    /// Timeline工具：镜头效果
    /// 模糊 ChromaticAberration
    /// 
    /// ——程一峰；2020.11.03
    /// </summary>
    public class HeroPerformChromaticAberration : PlayableBehaviour
    {
        /// <summary>
        /// 模糊强度；
        /// </summary>
        public float Intensity = 1;

        /// <summary>
        /// 光谱图；
        /// </summary>
        public Texture SpectralLut;

        /// <summary>
        /// 是否是关闭效果；
        /// </summary>
        public bool IsOff = false;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (CameraPostProcessingTool.Instance)
            {
                if (IsOff)
                    CameraPostProcessingTool.Instance.Do_CloseChromaticAberration();
                else
                    CameraPostProcessingTool.Instance.Do_OpenChromaticAberration(Intensity, SpectralLut);
            }
        }

    }
}
