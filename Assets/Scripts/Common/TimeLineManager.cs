using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timeline播放的管理器；
/// </summary>
public class TimeLineManager : MonoBehaviour
{
    [LabelText("编辑中相机")]
    public Camera InEditor_Camera;

    [LabelText("播放中相机")]
    public Camera Playing_Camera;

    private void Start()
    {
        InEditor_Camera.enabled = false;
        Playing_Camera.enabled = true;

        //设置为不受事件缩放影响；
        var arr = FindObjectsOfType<PlayableDirector>();
        foreach (var item in arr)
            item.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
    }

}
