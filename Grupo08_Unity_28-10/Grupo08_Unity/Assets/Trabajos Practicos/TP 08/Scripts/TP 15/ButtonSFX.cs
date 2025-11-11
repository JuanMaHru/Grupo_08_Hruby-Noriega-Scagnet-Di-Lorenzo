using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayCustom(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
