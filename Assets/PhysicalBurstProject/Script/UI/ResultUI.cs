using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI turn;

    [Inject]
    SystemSEPlayable sePlayer;

    PlayerInput input;

    public int TurnNum { set => turn.text += value.ToString(); }

    public string NextScene { get; set; }

    public void EnterAble()
    {
        input.SwitchCurrentActionMap("Enter");
    } 

    public async void OnConfirm(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            sePlayer.ConfirmSE();
            await UniTask.Delay(100);
            SceneManager.LoadScene(NextScene);
        }
    }
    

    // Use this for initialization
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}