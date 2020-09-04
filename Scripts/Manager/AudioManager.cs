using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
    public float m_nMusicVolume = 0.5f;
    [HideInInspector]
    public float m_nSoundVolume = 0.5f;
    public AudioSource m_MusicSource;

    private List<AudioSource> m_LisEffectSources = new List<AudioSource>();

    private int m_nMax = 10;

    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                GameObject obj = new GameObject("AudioManager", typeof(AudioManager));
                DontDestroyOnLoad(obj);
                m_Instance = obj.GetComponent<AudioManager>();
            }
            return m_Instance;
        }

    }

    private void Update()
    {
        m_MusicSource.volume = m_nMusicVolume;
        if (m_nMax <= m_LisEffectSources.Count)
        {
            for (int i = 0; i < m_LisEffectSources.Count; ++i)
            {
                if (m_LisEffectSources[i].isPlaying == false)
                {
                    Destroy(m_LisEffectSources[i].gameObject);
                    m_LisEffectSources.RemoveAt(i);
                    return;
                }

            }

        }

    }

    public void Init()
    {
        if (m_MusicSource == null)
        {
            m_MusicSource = gameObject.AddComponent<AudioSource>();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            m_nMusicVolume = PlayerPrefs.GetFloat("MusicVolume", m_nMusicVolume);
            m_nSoundVolume = PlayerPrefs.GetFloat("SoundVolume", m_nSoundVolume);
        }
    }

    public void WorldSound(AudioClip audioClip)
    {
        m_MusicSource.clip = audioClip;
        m_MusicSource.loop = true;
        m_MusicSource.Play();
    }

    public void PlayEffect(AudioClip audioClip)
    {
        AudioSource audio = null;

        if (m_LisEffectSources.Count > 0)
        {
            for (int i = 0; i < m_LisEffectSources.Count; ++i)
            {
                if (m_LisEffectSources[i].isPlaying == false)
                {
                    audio = m_LisEffectSources[i];
                    break;
                }
            }
        }

        if (audio != null)
        {
            audio.name = audioClip.name;
            audio.PlayOneShot(audioClip, m_nSoundVolume);
        }
        else
        {
            GameObject obj = new GameObject(audioClip.name, typeof(AudioSource));
            obj.transform.parent = gameObject.transform;
            audio = obj.GetComponent<AudioSource>();
            audio.PlayOneShot(audioClip, m_nSoundVolume);
            m_LisEffectSources.Add(audio);
        }
    }
}
