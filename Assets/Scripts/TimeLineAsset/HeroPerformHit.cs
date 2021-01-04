using UnityEngine;
using UnityEngine.Playables;

namespace Warfare.Game.Perform
{

    // A behaviour that is attached to a playable
    public class HeroPerformHit : PlayableBehaviour
    {

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            //此时触发伤害；
            Debug.Log("触发伤害！");
        }


    }
}
