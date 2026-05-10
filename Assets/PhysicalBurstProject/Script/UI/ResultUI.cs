using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class ResultUI : ConfirmCancelCatchAble
{
    [SerializeField]
    TextMeshProUGUI turn;

    [Inject]
    SystemSEPlayable sePlayer;

    [Inject]
    ICommandAdder commandAdder;

    public int TurnNum { set => turn.text += value.ToString(); }

    public string NextScene { get; set; }

    protected override InputMode SelfMode => InputMode.Result;

    public void EnterAble()
    {
        InputModeChangeToSelf();
    } 

    public override async void OnConfirm(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            sePlayer.ConfirmSE();
            await UniTask.Delay(100);
            commandAdder.GoAddCommand(NextScene);
        }
        base.OnConfirm(context);
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}