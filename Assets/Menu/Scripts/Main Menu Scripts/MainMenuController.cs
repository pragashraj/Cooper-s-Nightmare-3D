using UnityEngine;

public class MainMenuController : MonoBehaviour {
    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;
    public GameObject LoadGamePanel;
    public string newGameSceneName;
    public int quickSaveSlotID;

    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject levelPanel;

    private Animator anim;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void Start () {
        anim = GetComponent<Animator>();

        PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
    }

    public void StartOnClick()
    {
        MainOptionsPanel.SetActive(false);
        StartGameOptionsPanel.SetActive(true);

        anim.Play("buttonTweenAnims_on");
        playClickSound();
    }

    public void OptionsOnClick()
    {
        MainOptionsPanel.SetActive(true);
        StartGameOptionsPanel.SetActive(false);

        anim.Play("buttonTweenAnims_on");
        playClickSound();
    }

    public void NewGameOnClick()
    {
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);

        anim.Play("OptTweenAnim_on");
        playClickSound();
    }

    public void ContinueGameOnClick()
    {
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(true);
        mainPanel.SetActive(false);

        anim.Play("OptTweenAnim_on");
        playClickSound();
    }

    public void BackOnClick()
    {
        anim.Play("buttonTweenAnims_off");
        playClickSound();
    }

    public void GameOptionOnClick()
    {
        GamePanel.SetActive(true);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);
        mainPanel.SetActive(false);

        anim.Play("OptTweenAnim_on");
        playClickSound();
    }

    public void ControlOptionOnClick()
    {
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(true);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);
        mainPanel.SetActive(false);

        anim.Play("OptTweenAnim_on");
        playClickSound();
    }

    public void GfxOptionOnClick()
    {
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(true);
        LoadGamePanel.SetActive(false);
        mainPanel.SetActive(false);

        anim.Play("OptTweenAnim_on");
        playClickSound();
    }

    public void OptionsBackOnClick()
    {
        mainPanel.SetActive(true);
        anim.Play("OptTweenAnim_off");
        playClickSound();
    }

    public void playHoverClip()
    {
       
    }

    void playClickSound() {
        audioManager.Play("Click");
    }

    public void Quit()
    {
        Application.Quit();
    }
}