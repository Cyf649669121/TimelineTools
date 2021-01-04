using Sirenix.OdinInspector;
using UnityEngine;

public class RotCube : MonoBehaviour
{

    [LabelText("旋转速度")]
    public float RotateSpeed = 360;

    void Update()
    {
        Vector3 angle = transform.eulerAngles;
        angle.y += Time.deltaTime * RotateSpeed;
        angle.y = Mathf.Repeat(angle.y, 360);
        transform.eulerAngles = angle;
    }

}
