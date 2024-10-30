using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip bgm;

    [SerializeField]
    private float loopStart;

    [SerializeField] 
    private float loopEnd;

    [SerializeField]
    private AudioSource bgmSource;

    private CancellationTokenSource cts;

    public async UniTask PlayBGM()
    {
        cts = new CancellationTokenSource();

        bgmSource.clip = bgm;
        bgmSource.Play();

        await UniTask.WaitUntil(() => (bgmSource.time >= loopEnd), cancellationToken: cts.Token);
        bgmSource.Stop();

        while(!cts.Token.IsCancellationRequested)
        {
            bgmSource.time = loopStart;
            bgmSource.Play();

            await UniTask.WaitUntil(() => (bgmSource.time >= loopEnd), cancellationToken: cts.Token);
            bgmSource.Stop();
        }

    }

    //BGMループテスト用
    private async UniTask LoopTest()
    {
        cts = new CancellationTokenSource();

        bgmSource.clip = bgm;
        bgmSource.time = loopEnd - 5f;
        bgmSource.Play();

        await UniTask.WaitUntil(() => (bgmSource.time >= loopEnd), cancellationToken: cts.Token);
        bgmSource.Stop();

        bgmSource.time = loopStart;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        cts?.Cancel();
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }

    // Use this for initialization
    void Start()
    {
        //PlayBGM().Forget();
        //LoopTest().Forget();
    }

    // Update is called once per frame
    void Update()
    {

    }
}