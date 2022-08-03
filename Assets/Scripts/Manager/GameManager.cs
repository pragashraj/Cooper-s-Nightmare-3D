using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private GameObject mainMap;

    private AudioManager audioManager;

    private bool introEnd = false;
    private bool isMainMenuOpen = false;
    private bool isMainMapOpen = false;
    private bool levelEnd = false;

    public bool isIntroEnd { get => introEnd; set => introEnd = value; }

    public bool isLevelEnd { get => levelEnd; set => levelEnd = value; }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
	    if (introEnd && !levelEnd) {
	        HandleMainMenu();
            HandleMainmap();
        }
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    private void HandleMainmap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMainMapOpen = !isMainMapOpen;

            if (isMainMapOpen)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                PlayAudio("InventoryOpen");
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayAudio("InventoryClose");
            }

            mainMap.SetActive(isMainMapOpen);
        }
    }

    private void HandleMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMainMenuOpen = !isMainMenuOpen;

            if (isMainMenuOpen)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                PlayAudio("InventoryOpen");
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayAudio("InventoryClose");
            }

            HandleMainMenuActive(isMainMenuOpen);
            Cursor.visible = isMainMenuOpen;
        }
    }

    private void HandleMainMenuActive(bool active)
    {
        mainMenu.SetActive(active);
    }

    public void HandleGameEndMenuActive(bool active)
    {
        gameEndMenu.SetActive(active);
    }

    public void HandleNewGameOnClick()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleContinueOnClick()
    {
        PlayAudio("Click");
        HandleMainMenuActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleEndGame()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(0);
    }

    public void HandleRestartCurrentlevel()
    {
        PlayAudio("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}