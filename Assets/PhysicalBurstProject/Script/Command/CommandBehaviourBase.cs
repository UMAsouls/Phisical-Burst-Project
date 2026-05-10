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

    public string Description => cmd.Description;

    public float SelectPriority { get => cmd.SelectPriority; set => cmd.SelectPriority = value; }

    [Inject]
    protected IPawnGettable strage;

    [Inject]
    protected CameraControllable cameraController;

    [Inject]
    protected CameraChangeAble camerChanger;

    [Inject]
    protected SEPlayable sePlayer;

    private bool EffectEnd;

    public abstract UniTask DoAction(int pawnID);

    public abstract void SetCommand(int pawnID);

    public string GetTypeText() => cmd.GetTypeText();

    public ICommand Copy()
    {
        throw new System.NotImplementedException();
    }
}