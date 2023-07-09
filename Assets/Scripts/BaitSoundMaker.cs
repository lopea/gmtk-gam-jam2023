using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaitSoundMaker : MonoBehaviour
{
    public AudioSource bait;

    public AudioSource food;
    // Start is called before the first frame update
    void Awake()
    {
     
        
    }

    public void Play(bool isBait)
    {
        if (isBait)
        {
            var transform1 = transform;
            var audio = Instantiate(bait, transform1.position,transform1.rotation);
            audio.AddComponent<ShutupOnDeath>();
            Destroy(audio,6.0f);
        }
        else
        {
            var transform1 = transform;
            var audio = Instantiate(food, transform1.position,transform1.rotation);
            audio.AddComponent<ShutupOnDeath>();
            Destroy(audio, 5.0f);
        }
    }

}
