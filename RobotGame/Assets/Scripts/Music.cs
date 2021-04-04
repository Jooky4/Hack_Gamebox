using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    private AudioSource music;
    public Sprite muteSprite;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        music = GetComponent<AudioSource>();
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSprite()
    {
        img.sprite = muteSprite;
        img.SetNativeSize();
    }
    public void Mute()
    {
        
        if (music.isPlaying)
            music.Pause();
        else
            music.Play();
    }
}
