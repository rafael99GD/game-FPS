using UnityEngine;
using UnityEngine.Audio;

public enum AudioFx
{
    BallHit,
    PistolShot,
    PistolReload,
    DefeatBoss
}

public enum AudioMusic
{
    IntroMusic,
    BattleMusic,
    BossMusic,
    AmbienceMusic
}

public enum AudioAmbience
{
    BirdsChirping,
    Forest,
    Storm,
    Chimney
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip[] fxClips;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] ambienceClips;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambienceAudioSource;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayFx(AudioFx audioFx, AudioSource audioSource)
    {
        audioSource.PlayOneShot(fxClips[(int) audioFx]);
    }

    public void PlayMusic(AudioMusic audioMusic, bool isLooping = true)
    {
        musicAudioSource.loop = isLooping;
        musicAudioSource.clip = musicClips[(int) audioMusic];
        musicAudioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", SliderToFaderFloatConvertion(volume));
    }

    public void SetAmbienceVolume(float volume)
    {
        audioMixer.SetFloat("AmbienceVolume", SliderToFaderFloatConvertion(volume));
    }

    public void SetFxVolume(float volume)
    {
        audioMixer.SetFloat("FxVolume", SliderToFaderFloatConvertion(volume));
    }

    private float SliderToFaderFloatConvertion(float value)
    {
        return value * 80 - 80;
    }

}
