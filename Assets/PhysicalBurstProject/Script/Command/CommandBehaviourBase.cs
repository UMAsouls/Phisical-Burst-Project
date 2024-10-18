using Cysharp.Threading.Tasks;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CommandBehaviourBase<T> : MonoBehaviour, IActionCommandBehaviour where T: IActionCommand
{
    protected T cmd;

    protected bool burst;
    public bool IsBurst => burst;

    public string Name => cmd.Name;

    public float UseMana => cmd.UseMana;

    public abstract UniTask DoAction(int pawnID);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}