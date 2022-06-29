using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] private AudioClip ShootSound;
    [SerializeField] private AudioClip SkillSound;
    [SerializeField] private AudioClip CollectableSound;
    [SerializeField] private AudioClip CollectOrisusSound;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayShootSFX()
    {
        _audioSource.PlayOneShot(ShootSound);
    }
}
