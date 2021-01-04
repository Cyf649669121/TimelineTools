using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Warfare/CSO/CameraShakeConfig")]
public class CameraShakeConfig : SerializedScriptableObject
{
    [LabelText("抖动的频率秒")]
    public float shakeCD = 0.01f;
    [LabelText("抖动次数")]
    public int shakeCount = 15;
    [LabelText("抖动幅度")]
    public float shakeDegree = 2f;
    [LabelText("每次抖动的衰减比例")]
    public float fadeRate = 1f;
    [LabelText("抖动轴比例")]
    public Vector3 axis = new Vector3(2, 2, 0);
    [LabelText("重复抖动次数")]
    public int mulityCount = 1;
    [LabelText("重复抖动间隔秒")]
    public float mulityDelay = 0;

    [LabelText("是否开启触发距离和衰减距离")]
    public bool isOpenFadeTrigger = false;
    [LabelText("触发距离")]
    public float triggerDis = 800;
    [LabelText("衰减距离")]
    public float fadeDis = 300;
}
