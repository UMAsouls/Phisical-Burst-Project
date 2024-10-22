using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class BattleActionUnit : MonoBehaviour
{
    public async UniTask Battle(IBattleCommand[] cmds, AttackAble battlePawn, PawnActInterface pawn)
    {
        await battlePawn.EmergencyBattle();

        for (int i = 0; i < cmds.Length; i++)
        {
            
        }
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