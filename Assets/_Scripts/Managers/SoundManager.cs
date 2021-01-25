using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundHelper soundHelper;
    private static AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundHelper = GetComponent<SoundHelper>();

        audioSource.volume = GameController.settings.SoundIsOn ? 1 : 0;
    }

    public static bool Volume
    {
        get
        {
            return audioSource.volume == 1;
        }

        set
        {
            audioSource.volume = value ? 1 : 0;
        }
    }

    public static void Play(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.WhenScorePP:
                audioSource.PlayOneShot(soundHelper.WhenScorePP); break;
        }
    }

    public static void Stop()
    {
        if (!IsPlaying)
            return;
        audioSource.Stop();
    }

    public static bool IsPlaying
    {
        get
        {
            return audioSource.isPlaying;
        }
    }

    public enum SoundType
    {
        WhenScorePP
    }
}
