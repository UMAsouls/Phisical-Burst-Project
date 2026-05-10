using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Zenject.SpaceFighter;
using Zenject;
using System.Threading.Tasks;

public class AddCommandSystem : ConfirmCancelCatchAble
{
    [SerializeField]
    AutoCmdUIController autoCmdUIController;

    [SerializeField]
    CmdInfoWindow cmdInfoWindow;

    [SerializeField]
    PawnFullStatusUI fullStatusUI;

    [SerializeField]
    List<PawnPackage> pacakages;

    [Inject]
    ICommandAdder adder;

    [Inject]
    ICommandStrage cmdStrage;

    [Inject]
    BGMPlayable player;

    int idx = 0;

    ICommand[] commands;

    List<(string, CommandType, int)> addCmdIndices;

    protected override InputMode SelfMode => InputMode.FirstSelect;

    protected void SetInfoWindow(ICommand cmd)
    {
        cmdInfoWindow.Name = cmd.Name;
        cmdInfoWindow.Description = cmd.Description;
        cmdInfoWindow.UseMana = cmd.UseMana.ToString();
        cmdInfoWindow.Type = cmd.GetTypeText();
    }

    protected void Move(int dir)
    {
        idx = (idx + dir) % commands.Length;
        if(idx < 0) idx = commands.Length - 1;
        autoCmdUIController.Move(dir);
        SetInfoWindow(commands[idx]);
    }

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Vector2 moveInput = context.ReadValue<Vector2>();
        systemSEPlayer.SelectorMoveSE();

        //input上ではup > 0 down < 0
        if (moveInput.y > 0) Move(-1);
        if (moveInput.y < 0) Move(1);
    }

    protected override void SetAllAction()
    {
        SetAction("Move", OnSelectorMove);
        base.SetAllAction();
    }

    protected void FirstSet()
    {
        foreach (var cmd in commands)
        {
            autoCmdUIController.CmdAdd(cmd.Name);
        }
        autoCmdUIController.Set(0);
        idx = 0;
        SetInfoWindow(commands[idx]);
        isConfirm = false;
        isCancel = false;
    }

    protected async UniTask<bool> SelectAddCommand(
        PawnPackage package, AddCommand RemoveCmd
    )
    {
        var rmActionCmds = RemoveCmd.AddActionCmdList;
        var rmBattleCmds = RemoveCmd.AddBattleCmdList;

        rmActionCmds.Sort();
        rmBattleCmds.Sort();

        var addActionCmdBase = package.AddActionCmds;
        var addBattleCmdBase = package.AddBattleCmds;

        Debug.Log($"rmActions: {rmActionCmds.Count}");
        Debug.Log($"rmBattles: {rmBattleCmds.Count}");

       var addActionCmds = new CommandPackage[addActionCmdBase.Length - rmActionCmds.Count];
       var addBattleCmds = new CommandPackage[addBattleCmdBase.Length - rmBattleCmds.Count];

        int h1 = 0;
        int h2 = 0;
        for (int i = 0; i < addActionCmdBase.Length; i++)
        {
            if(rmActionCmds.Count <= i)
            {
                addActionCmds[h2++] = addActionCmdBase[i];
                continue;
            }
            if (rmActionCmds[h1] == i) { h1++; continue; }
            addActionCmds[h2++] = addActionCmdBase[i];
        }

        h1 = 0;
        h2 = 0;
        for (int i = 0; i < addBattleCmdBase.Length; i++)
        {
            if (rmBattleCmds.Count <= i)
            {
                addBattleCmds[h2++] = addBattleCmdBase[i];
                continue;
            }
            if (rmBattleCmds[h1] == i) { h1++; continue; }
            addBattleCmds[h2++] = addBattleCmdBase[i];
        }

        var cmd1 = cmdStrage.GetActCmds(addActionCmds);
        var cmd2 = cmdStrage.GetBattleCmds(addBattleCmds);

        commands = new ICommand[cmd1.Length + cmd2.Length];
        cmd1.CopyTo(commands, 0);
        cmd2.CopyTo(commands, cmd1.Length);

        if(commands.Length == 0)
        {
            addCmdIndices.Add((package.Name, CommandType.ActionCommand, -1));
            return true;
        }

        FirstSet();
        
        await UniTask.WaitUntil(() => isConfirm || isCancel, cancellationToken: destroyCancellationToken);
        
        if(isConfirm)
        {
            int trueidx;
            CommandType type;
            if (idx < cmd1.Length)
            {
                type = CommandType.ActionCommand;
                trueidx = idx;
            } else
            {
                type = CommandType.BattleCommand;
                trueidx = idx - cmd1.Length;
            }
            addCmdIndices.Add((package.Name, type, trueidx));
        }

        return isConfirm;
    }


    // Use this for initialization
    public override async void Start()
    {
        player.PlayBGM();
        base.Start();
        addCmdIndices = new List<(string, CommandType, int)>();
        int pidx = 0;
        InputModeChangeToSelf();
        while(pidx < pacakages.Count) 
        {
            autoCmdUIController.reset();
            var p = pacakages[pidx];
            fullStatusUI.Set(p.Status);
            bool select = await SelectAddCommand(p, adder.GetCommandList(p.Name));

            if (select) pidx++;
            else pidx = Mathf.Clamp(pidx - 1, 0, pacakages.Count);
        }

        foreach (var adds in addCmdIndices)
        {
            if(adds.Item3 < 0) continue;
            adder.AddCommand(adds.Item1, adds.Item3, adds.Item2);
        }
        adder.GoNextScene();
    }
}