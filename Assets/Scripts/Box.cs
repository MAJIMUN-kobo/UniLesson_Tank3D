using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Box : MonoBehaviour
{
    private CancellationTokenSource cts = new();
    private int threadCount = 0;

    public async void Start()
    {
        await TeleportAsync();
        cts.Cancel();
        cts.Dispose();
    }

    public async Task TeleportAsync()
    {
        var token = cts.Token;

        await Task.Run(() => {
            if (!token.IsCancellationRequested)
                for (int i = 0; i < 10000; i++)
                {
                    threadCount++;
                    Debug.Log($"{ threadCount } メインとは別スレッドで動いてるから大量ループでもゲームが動く"); 
                }

        }, token);
        await Task.Delay(2000);

        //transform.position = new Vector3( 0, 5, 10 );
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }

    void OnCollisionEnter( Collision collision )
    {
        if( collision.transform.tag.Equals( "Bullet" ) )
        {
            Destroy( gameObject );
        }
    }
}
