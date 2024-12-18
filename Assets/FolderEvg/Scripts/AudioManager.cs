using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip bgLavaStill;
    public AudioClip bgLavaRush;
    public AudioClip catWalk;
    public AudioClip catJump;
    public AudioClip catFall;
    public AudioClip catPush;
    public AudioClip catBurn;
    public AudioClip rockPush;
    public AudioClip rockFall;
    public AudioClip fallInLava;
    public AudioClip sheepBurn;

    private void Start()
    {
        musicSource.clip = bgLavaStill;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sFX)
    {
        SFXSource.PlayOneShot(sFX);
    }

    public void MusicChange()
    {
        musicSource.Stop();
        musicSource.clip = bgLavaRush;
        musicSource.Play();
    }
}
