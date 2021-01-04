/*
 *Copyright(C) 2020 by  GYYX All rights reserved.
 *Unity版本：2018.4.23f1 
 *作者:程一峰  
 *创建日期: 2020-11-30 
 *模块说明：
 *版本: 1.2
*/


namespace Warfare.KWarCamera
{

    /// <summary>
    /// 相机行动的类型；
    /// 
    /// 先让所有的场景都用这个就好了。
    /// </summary>
    public enum E_CmeraActionType
    {
        Default = 0,

        /// <summary>
        /// 通过技能拉起相机；
        /// </summary>
        BATTLE_PullUpCamera_BySkill,

        /// <summary>
        /// 通过方阵拉起相机；
        /// </summary>
        BATTLE_PullUpCamera_BySquad,

        BATTLE_FocusToTarget,

        BATTLE_Revert,

        /// <summary>
        /// 边缘的移动控制；
        /// </summary>
        BATTLE_AdjustMove,

        /// <summary>
        /// 切换点位
        /// </summary>
        BATTLE_SwicthPoint,

        /// <summary>
        /// 开始战斗的移动；
        /// </summary>
        BATTLE_SwitchRunningStart,

        /// <summary>
        /// 战斗结束
        /// </summary>
        BATTLE_BattleEnd,

        /// <summary>
        /// 主城：聚焦某建筑
        /// </summary>
        MainCity_FocusToTarget,

        /// <summary>
        /// 主城，相机还原；
        /// </summary>
        MainCity_MoveWithPosAndRot,

        /// <summary>
        /// 关UI时的还原；
        /// </summary>
        MainCity_RevertByUI,

        /// <summary>
        /// 移动到区域建造的固定点；
        /// </summary>
        MainCity_MoveToAreaFixPos,

        /// <summary>
        /// 设定了固定时间的移动模式；
        /// </summary>
        Guide_ActionWithTime,

        /// <summary>
        /// 大地图的移动；
        /// </summary>
        World_MoveToPoint,

        /// <summary>
        /// 大地图的直接移动；
        /// 有便宜；
        /// </summary>
        World_MoveToPoint_Direct,

        /// <summary>
        /// 一种动画控制？
        /// </summary>
        World_SwitchScene,
    }

}
