using UnityEngine;
using UnityEngine.Playables;
using Warfare.KWarCamera;

namespace Warfare.Game.Perform
{

    /// <summary>
    /// TimeLine 工具：相机抖动；
    /// 
    /// ——程一峰；2020.11.07
    public class HeroPerformCameraShake_Behaviour : PlayableBehaviour
    {
        public CameraShakeConfig shakeConfig;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif

            if (shakeConfig)
                CameraShake.Get?.ShowShake(shakeConfig);
        }

    }

}
