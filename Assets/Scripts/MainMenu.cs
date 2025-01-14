using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _menuSelectClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayMenuSelect()
    {
        _audioSource.PlayOneShot(_menuSelectClip);
    }
}
