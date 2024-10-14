using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using Zenject;

public class CommandBehaviourMaker : MonoBehaviour, CommandBehaviourMakeable
{

    public async UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd)
    {
        

        return null;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}