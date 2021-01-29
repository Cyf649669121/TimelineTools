using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace Warfare.Game.Perform
{

    /// <summary>
    /// TimeLine 工具：相机抖动；
    /// 
    /// ——程一峰；2020.11.07
    /// </summary>
    [System.Serializable]
    public class HeroPerformCameraShake : PlayableAsset
    {
        /// <summary>
        /// 相机震动的配置；
        /// </summary>
        [LabelText("震动配置")]
        public CameraShakeConfig shakeConfig;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<HeroPerformCameraShake_Behaviour>.Create(graph);
            playable.GetBehaviour().shakeConfig = shakeConfig;
            return playable;
        }
    }
}
