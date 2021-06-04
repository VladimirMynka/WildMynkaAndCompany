using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideAudio : MonoBehaviour
{
    public AudioClip[] sounds;
    public float timing;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        if (time > timing + 1) time = 0;
        if (time != 0) time += Time.deltaTime;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other) {
        if (sounds.Length > 0 && time == 0){
            int rand = Random.Range(0, sounds.Length - 1);
            GetComponent< AudioSource >().PlayOneShot(sounds[rand]);
            time += 1;
        }
    }
}
