using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CommandBehaviourMaker : MonoBehaviour, CommandBehaviourMakeable
{

    Dictionary<ActionCmdType, CommandMakerBase<IActionCommand>> dict;

    HealMaker healMaker;

    public async UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd)
    {
        ActionCmdType type  = cmd.Type;

        switch(type)
        {
            case ActionCmdType.Heal:
                await healMaker.MakeBehaviour(cmd.GetMySelf<IHealCommand>()); break;
        }

        return null;
    }


    // Use this for initialization
    void Start()
    {
        dict = new Dictionary<ActionCmdType, CommandMakerBase<IActionCommand>>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}