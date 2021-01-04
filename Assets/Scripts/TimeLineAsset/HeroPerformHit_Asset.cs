using UnityEngine;
using UnityEngine.Playables;

namespace Warfare.Game.Perform
{
    /// <summary>
    /// 军团战技能特写的触发命中效果；
    /// 
    /// ——程一峰；2020.11.02
    /// </summary>
    [System.Serializable]
    public class HeroPerformHit_Asset : PlayableAsset
    {
        /// <summary>
        /// 不启用；
        /// </summary>
        [Header("勾选使其失效")]
        public bool Disable;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            if (!Disable)
            {
                return ScriptPlayable<HeroPerformHit>.Create(graph);
            }
            else
            {
                return Playable.Create(graph);
            }
        }

    }
}