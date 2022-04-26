using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterAnimationController : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    private Animator anim;
    private AudioManager audioManager;
	
    private void Awake() 
    {
	audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnMouseOver()
    {
        anim.enabled = true;
    }

    public void OnMouseExit()
    {
        anim.enabled = false;
    }

    public void ChapterOnClick() 
    {
        playClickSound();
        SceneManager.LoadScene(sceneToLoad);
    }

    void playClickSound()
    {
        audioManager.Play("Click");
    }
}