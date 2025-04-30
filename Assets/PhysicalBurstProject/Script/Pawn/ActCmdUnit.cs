using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class ActCmdUnit : MonoBehaviour
{

    public async UniTask Action(IActionCommandBehaviour act, int pawnID)
    {
        await act.DoAction(pawnID);
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