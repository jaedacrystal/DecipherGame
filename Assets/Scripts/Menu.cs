using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuCanvas;

    [Header("Scenes to Hide Menu")]
    [SerializeField] private List<string> scenesToHideMenu;

    private static Menu instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        menu.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckScene();
    }

    public void CheckScene()
    {
        bool shouldHide = scenesToHideMenu.Contains(SceneManager.GetActiveScene().name);

        menuCanvas.SetActive(!shouldHide);
    }

    public void ToggleMenu()
    {
        if (scenesToHideMenu.Contains(SceneManager.GetActiveScene().name)) return;

        bool isMenuActive = !menu.activeSelf;
        menu.SetActive(isMenuActive);
        Time.timeScale = isMenuActive ? 0 : 1;
        SoundFX.Play("Click");
    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        SoundFX.Play("Click");
    }

    public void ExitGame()
    {
        SoundFX.Play("Click");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Show()
    {
        menu.gameObject.SetActive(true);
    }

    public void Hide()
    {
        menu.gameObject.SetActive(false);
    }

}
