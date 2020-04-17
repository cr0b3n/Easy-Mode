using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    //[Header("Player Audio Clips")]
    //public AudioClip playerStepsClip;
    //public AudioClip playerAttackClip;
    //public AudioClip playerJumpClip;
    //public AudioClip playerLandClip;
    //public AudioClip playerDashClip;
    //public AudioClip playerHitClip;

    [Header("Effects Audio Clips")]
    public AudioClip gameOverClip;
    public AudioClip levelCompleteClip;

    [Header("UI Audio Clips")]
    public AudioClip regButtonAudio;
    public AudioClip startAudioClip;
    public AudioClip transitionAudio;

    [Header("BGM")]
    [Range(0.0f, 1.0f)]
    public float bgmVolume = 0.3f;
    public AudioClip gamePlayClip;
    public AudioClip menuBGMClip;

    private AudioSource bgmSource;
    private AudioSource uiSource;
    private AudioSource playerSource;
    private AudioSource effectSource;


    private void Awake() {

        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        uiSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        effectSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        uiSource.ignoreListenerPause = true;
        bgmSource.volume = bgmVolume;
    }

    public static void StopBGM() {

        if (instance == null) return;

        instance.bgmSource.Stop();
    }

    public static void PlayBGM(BGMType bgmType) {

        if (instance == null) return;

        instance.bgmSource.Stop();

        switch (bgmType) {

            case BGMType.Gameplay:
                instance.bgmSource.clip = instance.gamePlayClip;
                break;
            case BGMType.Menu:
                instance.bgmSource.clip = instance.menuBGMClip;
                break;
        }

        instance.bgmSource.loop = true;
        instance.bgmSource.Play();
    }

    public static void PlayAudioEffect(EffectAudio effectAudio) {

        if (instance == null) return;

        if (instance.effectSource.isPlaying)
            instance.effectSource.Stop();

        switch (effectAudio) {

            case EffectAudio.LevelComplete:
                instance.effectSource.PlayOneShot(instance.levelCompleteClip);
                break;
            case EffectAudio.GameOver:
                instance.bgmSource.Stop();
                instance.effectSource.PlayOneShot(instance.gameOverClip, 0.45f);
                break;
        }
    }

    //public static void PlayPlayerAudio(PlayerAudio playerAudio) {

    //    if (instance == null) return;

    //    //if (current.playerSource.isPlaying)
    //    //    current.playerSource.Stop();

    //    switch (playerAudio) {
    //        case PlayerAudio.Step:
    //            instance.playerSource.PlayOneShot(instance.playerStepsClip);
    //            break;
    //        case PlayerAudio.Jump:
    //            instance.playerSource.PlayOneShot(instance.playerJumpClip);
    //            break;
    //        case PlayerAudio.Land:
    //            instance.playerSource.PlayOneShot(instance.playerLandClip);
    //            break;
    //        case PlayerAudio.Attack:
    //            instance.playerSource.PlayOneShot(instance.playerAttackClip);
    //            break;
    //        case PlayerAudio.Dash:
    //            instance.playerSource.PlayOneShot(instance.playerDashClip);
    //            break;
    //        case PlayerAudio.Hit:
    //            instance.playerSource.PlayOneShot(instance.playerHitClip);
    //            break;
    //    }
    //}

    public static void StopAllAudio() {

        if (instance == null) return;

        instance.playerSource.Stop();
        instance.effectSource.Stop();
    }

    public static void PlayUIAudio(GUIAudio gUIAudio) {

        if (instance == null) return;

        if (instance.uiSource.isPlaying)
            instance.uiSource.Stop();

        switch (gUIAudio) {
            case GUIAudio.RegButton:
                instance.uiSource.PlayOneShot(instance.regButtonAudio);
                break;
            case GUIAudio.Transition:
                instance.uiSource.PlayOneShot(instance.transitionAudio);
                break;
            case GUIAudio.StartButton:
                instance.uiSource.PlayOneShot(instance.startAudioClip, 0.75f);
                break;
        }
    }
}

public enum GUIAudio {
    RegButton,
    StartButton,
    Transition
}

public enum EffectAudio {
    LevelComplete,
    GameOver
}

public enum BGMType {
    Gameplay,
    Menu,
}

//public enum PlayerAudio {
//    Step,
//    Jump,
//    Land,
//    Attack,
//    Hit,
//    Dash
//}