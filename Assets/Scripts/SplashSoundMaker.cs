using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSoundMaker : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;


    public void Splash()
    {
        var transform1 = transform;
        var s = Instantiate(source, transform1.position, transform1.rotation);
        Destroy(s.gameObject, 5.0f);
        Debug.Log("splash!");
    }
}
