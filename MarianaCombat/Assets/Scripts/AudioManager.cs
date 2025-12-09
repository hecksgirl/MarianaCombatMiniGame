using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public GameObject weaponSourcePrefab;
    public AudioClip[] swordSwingClip;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void PlaySwordSwing()
    {
        int index = Random.Range(0, swordSwingClip.Length);
        AudioSource weaponSource = Instantiate(weaponSourcePrefab, transform).GetComponent<AudioSource>();
        weaponSource.clip = swordSwingClip[index];
        weaponSource.Play();
    }
}
