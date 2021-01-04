using UnityEngine;
using UnityEngine.Playables;

namespace Warfare.Game.Perform
{
    /// <summary>
    /// TimeLine 配置；
    /// 时间缩放；
    /// 
    /// ——程一峰；2020.11.03
    /// </summary>
    public class HeroPerformTimeScale : PlayableBehaviour
    {

        /// <summary>
        /// 缩放比例；
        /// </summary>
        public float TimeScale;

        /// <summary>
        /// 是否关闭；
        /// 
        /// 关闭就是恢复全局设置；
        /// </summary>
        public bool IsOff;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
#if UNITY_EDITOR

            if (!Application.isPlaying)
                return;
#endif

            if (IsOff)
                Time.timeScale = 1;
            else
                Time.timeScale = TimeScale;
        }

    }
}
