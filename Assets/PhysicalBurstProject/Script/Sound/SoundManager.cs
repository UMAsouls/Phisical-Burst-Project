using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour, SystemSEPlayable, BGMPlayable
{

    [SerializeField]
    BGMPlayer bgmPlayer;

    [SerializeField]
    SEPlayer sePlayer;

    [SerializeField]
    AudioClip Confirm;

    [SerializeField]
    AudioClip Cancel;

    [SerializeField]
    AudioClip ConfirmBlock;

    [SerializeField]
    AudioClip SelectorMove;

    [SerializeField]
    AudioClip BigDamage;

    [SerializeField]
    AudioClip Damage;

    [SerializeField]
    AudioClip Dodge;

    [SerializeField]
    AudioClip Burst;

    [SerializeField]
    AudioClip Stun;

    [SerializeField]
    AudioClip BattleAlarm;

    [SerializeField]
    AudioClip Death;

    public void PlayBGM()
    {
        bgmPlayer.PlayBGM().Forget();
    }

    public void StopBGM()
    {
        bgmPlayer.StopBGM();
    }

    public void PlaySE(AudioClip clip)
    {
        sePlayer.PlaySE(clip);
    }

    public void ConfirmSE() => PlaySE(Confirm);
    public void CancelSE() => PlaySE(Cancel);
    public void BlockSE() => PlaySE(ConfirmBlock);
    public void SelectorMoveSE() => PlaySE(SelectorMove);
    public void BigDamageSE() => PlaySE(BigDamage);
    public void DamageSE() => PlaySE(Damage);
    public void DodgeSE() => PlaySE(Dodge);
    public void BurstSE() => PlaySE(Burst);
    public void StunSE() => PlaySE(Stun);
    public void BattleAlarmSE() => PlaySE(BattleAlarm);
    public void DeathSE() => PlaySE(Death);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}