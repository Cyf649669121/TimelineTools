using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace Warfare.Game.Perform
{
    /// <summary>
    /// Timeline工具：镜头效果
    /// 模糊 ChromaticAberration
    /// 
    /// ——程一峰；2020.11.03
    /// </summary>
    [System.Serializable]
    public class HeroPerformChromaticAberration_Asset : PlayableAsset
    {

        [LabelText("是否关闭")]
        public bool IsOff = false;

        /// <summary>
        /// 模糊强度；
        /// </summary>
        [Range(0, 10), LabelText("强度")]
        [HideIf("IsOff", true)]
        public float Intensity = 1;

        /// <summary>
        /// 光谱图；
        /// </summary>
        [HideIf("IsOff", true)]
        public Texture SpectralLut;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<HeroPerformChromaticAberration>.Create(graph);
            playable.GetBehaviour().Intensity = Intensity;
            playable.GetBehaviour().SpectralLut = SpectralLut;
            playable.GetBehaviour().IsOff = IsOff;
            return playable;
        }

    }
}
