using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    public AudioSource titlePlayer;
    AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    public static AudioManager Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            if (instance == null)
                Debug.Log("No Audio Manager");

        }
        return instance;
    }

    private void Awake()
    {
        Init();
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject _bgmObject = new GameObject("BGMPlayer");
        _bgmObject.transform.parent = transform;
        bgmPlayer = _bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].bypassListenerEffects = true;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int _loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[_loopIndex].isPlaying)
                continue;

            channelIndex = _loopIndex;
            sfxPlayers[_loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[_loopIndex].Play();
            break;
        }

    }

    public void PlayBGM(bool isPlaying)
    {
        if (isPlaying)
        {
            titlePlayer.Stop();
            bgmPlayer.Play();
        }
        else
            bgmPlayer.Stop();
    }

    public void EffectBGM(bool isPlaying)
    {
        bgmEffect.enabled = isPlaying;
    }
}
