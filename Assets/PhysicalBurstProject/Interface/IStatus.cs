using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public interface IStatus
{
    public int HP { get; }

    public int MaxHP { get; }

    public string Name { get; }

   /// <summary>
   /// 現在の攻撃力
   /// </summary>
    public float Attack { get; }
    /// <summary>
    /// 攻撃力の元の数値
    /// </summary>
    public float AttackBase { get; }

    /// <summary>
    /// 現在の防御力
    /// </summary>
    public float Defence { get; }
    /// <summary>
    /// 防御力の元の数値
    /// </summary>
    public float DefenceBase { get; }

    /// <summary>
    /// 現在の行動の速さ
    /// </summary>
    public float Speed { get; }
    /// <summary>
    /// 速さの元の数値
    /// </summary>
    public float SpeedBase { get; }

    /// <summary>
    /// 現在の行動可能範囲
    /// </summary>
    public float Range { get; }
    /// <summary>
    /// 元の行動可能範囲
    /// </summary>
    public float RangeBase { get; }

    public float AttackRange { get; }

    public float AttackRangeBase { get; }

    public void init();

}