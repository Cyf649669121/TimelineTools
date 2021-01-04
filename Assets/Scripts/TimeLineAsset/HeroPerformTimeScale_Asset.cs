using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace Warfare.Game.Perform
{
    /// <summary>
    /// Timeline 工具： 时间缩放；
    /// 
    /// ——程一峰；2020.11.03
    /// </summary>
    [System.Serializable]
    public class HeroPerformTimeScale_Asset : PlayableAsset
    {
        [LabelText("恢复全局设置")]
        public bool IsOff = false;

        [Range(0, 1), LabelText("缩放比例")]
        [HideIf("IsOff", true)]
        public float TimeScale = 0.5f;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<HeroPerformTimeScale>.Create(graph);
            playable.GetBehaviour().TimeScale = TimeScale;
            playable.GetBehaviour().IsOff = IsOff;
            return playable;
        }

    }
}