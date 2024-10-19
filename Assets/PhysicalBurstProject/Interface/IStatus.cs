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
   /// ���݂̍U����
   /// </summary>
    public float Attack { get; }
    /// <summary>
    /// �U���͂̌��̐��l
    /// </summary>
    public float AttackBase { get; }

    /// <summary>
    /// ���݂̖h���
    /// </summary>
    public float Defence { get; }
    /// <summary>
    /// �h��͂̌��̐��l
    /// </summary>
    public float DefenceBase { get; }

    /// <summary>
    /// ���݂̍s���̑���
    /// </summary>
    public float Speed { get; }
    /// <summary>
    /// �����̌��̐��l
    /// </summary>
    public float SpeedBase { get; }

    /// <summary>
    /// ���݂̍s���\�͈�
    /// </summary>
    public float Range { get; }
    /// <summary>
    /// ���̍s���\�͈�
    /// </summary>
    public float RangeBase { get; }

    public float AttackRange { get; }

    public float AttackRangeBase { get; }

    public void init();

}