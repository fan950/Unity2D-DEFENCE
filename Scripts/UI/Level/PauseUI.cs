using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseUI : MonoBehaviour
{
    public Slider m_MusicVolum;
    public Slider m_SoundVolum;

    private float m_fMusicVolum;
    private float m_fSoundVolum;

    public AudioClip audioClip;

    private bool m_bOneTouch;

    public void OnChangeSound(float value)
    {
        m_SoundVolum.value = value;
    }
    public void OnChangeMusic(float value)
    {
        m_MusicVolum.value = value;
    }
    
    private void Start()
    {
        m_fMusicVolum = PlayerPrefs.GetFloat("MusicVolume", AudioManager.Instance.m_nMusicVolume);
        m_fSoundVolum = PlayerPrefs.GetFloat("SoundVolume", AudioManager.Instance.m_nSoundVolume);

        m_MusicVolum.value =PlayerPrefs.GetFloat("MusicVolume", AudioManager.Instance.m_nMusicVolume);
        m_SoundVolum.value = PlayerPrefs.GetFloat("SoundVolume", AudioManager.Instance.m_nSoundVolume);

        m_SoundVolum.onValueChanged.AddListener(OnChangeSound); 
        m_MusicVolum.onValueChanged.AddListener(OnChangeMusic);

        m_bOneTouch = false;
    }

    private void Update()
    {
        AudioManager.Instance.m_nMusicVolume = m_MusicVolum.value;
        AudioManager.Instance.m_nSoundVolume = m_SoundVolum.value;
    }

    public void pause()
    {
        AudioManager.Instance.PlayEffect(audioClip);
        SeeUI.m_nPause = false;

        m_MusicVolum.value = m_fMusicVolum;
        m_SoundVolum.value = m_fSoundVolum;

        Destroy(gameObject);
    }

    public void Apply()
    {
        AudioManager.Instance.PlayEffect(audioClip);
        SeeUI.m_nPause = false;

        Destroy(gameObject);

        PlayerPrefs.SetFloat("MusicVolume", AudioManager.Instance.m_nMusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", AudioManager.Instance.m_nSoundVolume);
    }

    public void BackBtn()
    {
        if (m_bOneTouch==false)
        {
            AudioManager.Instance.PlayEffect(audioClip);
            if (SceneMng.m_uIScreen != null)
            {
                SceneMng.m_uIScreen.Execute(true);
            }
            SceneMng.Instance.NextScene(SceneChange.SelectScene, 1.5f);
            m_bOneTouch = true;
        }

    }
}
