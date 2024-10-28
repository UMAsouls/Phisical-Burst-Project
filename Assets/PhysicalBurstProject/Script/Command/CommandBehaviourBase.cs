using Cysharp.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public abstract class CommandBehaviourBase<T> : IActionCommandBehaviour where T: IActionCommand
{
    protected T cmd;

    protected bool burst;
    public bool IsBurst => burst;

    protected PawnType target;

    public string Name => cmd.Name;

    public float UseMana => cmd.UseMana;

    public float SelectPriority => cmd.SelectPriority;

    [Inject]
    protected IPawnGettable strage;

    [Inject]
    protected CameraControllable cameraController;

    [Inject]
    protected CameraChangeAble camerChanger;

    private bool EffectEnd;

    public abstract UniTask DoAction(int pawnID);

    
}