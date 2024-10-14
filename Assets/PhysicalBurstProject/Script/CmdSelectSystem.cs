using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class CmdSelectSystem : ICmdSelectSystem
{
    [Inject]
    IPawnGettable strage;

    [Inject]
    ICmdSelectUIPrinter uiPrinter;

    public async UniTask CmdSelect(int id)
    {
        CommandActionSettable pawn = strage.GetPawnById<CommandActionSettable>(id);

        ICommand[] commands = pawn.GetActionCommands();

        string[] names  = new string[commands.Length];
        for (int i = 0; i < commands.Length; i++)  names[i] = commands[i].Name;

        uiPrinter.PrintCmdSelecter(names);

        
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