
namespace Warfare.KWarCamera
{

    /*
     * 这个枚举用来区分各种不同类型命令；
     * 不过目前就算命令类型不同也没有做特殊处理；
     * 
     * 后面可以根据不同的命令执行不同的方案；
     * 例如相机抖动的话，就可以在原有命令上叠加一个值；
     * 
     * 最开始还想优先级也用枚举的值类区分，但还是算了，不太好；
     * 
     * ——程一峰；2020.10.31
    */

    /// <summary>
    /// 相机控制命令类型；
    /// </summary>
    public enum E_CameraCmd
    {

        None = 0,

        /// <summary>
        /// 锁定位置和旋转
        /// </summary>
        LockPosAndRot = 1,

        /// <summary>
        /// 附加在当前状态下；
        /// </summary>
        Additional = 2,

    }

}
