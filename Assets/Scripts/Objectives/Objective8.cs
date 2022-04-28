using System.Collections;
using UnityEngine;

public class Objective8 : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject flora;
    [SerializeField] private GameObject playerY;
    [SerializeField] private GameObject floraY;
    [SerializeField] private GameObject[] cutCams;
    [SerializeField] private GameObject swat;
    [SerializeField] private float runningSpeed = 0f;
    [SerializeField] private float forwardZ = 1f;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject levelEnd;
    [SerializeField] private GameObject cinematicAlien1;
    [SerializeField] private GameObject cinematicAlien2;
    [SerializeField] private GameObject alienCompund;

    private ObjectiveManager objectiveManager;
    private bool storyMode = false;

    private void Awake()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    private void Update()
    {
        if (storyMode)
        {
            HandleRun();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveManager.LastCompletedObjective obj = objectiveManager.GetLastCompletedObjective();

            if (obj.Equals(ObjectiveManager.LastCompletedObjective.OBJ7))
            {
                fadeOutPanel.SetActive(true);
                fadeOutPanel.GetComponent<Animation>().Play();
                CompleteObject8();
                StartCoroutine(HandleObjectveFlow());
            }
        }
    }

    private void CompleteObject8()
    {
        objectiveManager.SetLastCompletedObjective(ObjectiveManager.LastCompletedObjective.OBJ8);
        objectiveManager.SetObjectivePanel("Objective 8 completed", 8);
    }

    IEnumerator HandleObjectveFlow()
    {
        yield return new WaitForSeconds(3f);
        flora.SetActive(false);
        player.SetActive(false);
        cutCams[0].SetActive(true);
        swat.GetComponent<Animator>().SetTrigger("Wave");
        floraY.SetActive(true);
        playerY.SetActive(true);
        storyMode = true;

        yield return new WaitForSeconds(1f);
        cutCams[0].GetComponent<Animation>().Play();

        yield return new WaitForSeconds(1f);
        fadeOutPanel.SetActive(false);

        yield return new WaitForSeconds(3.5f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        cutCams[0].SetActive(false);
        cutCams[1].SetActive(true);
	cinematicAlien1.SetActive(false);
	cinematicAlien2.SetActive(true);
	alienCompund.SetActive(true);

        yield return new WaitForSeconds(6.5f);
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(3f);
        blackScreen.SetActive(true);
        floraY.SetActive(false);
        playerY.SetActive(false);
        storyMode = false;

        yield return new WaitForSeconds(2f);
        levelEnd.SetActive(true);
        blackScreen.SetActive(false);
	cutCams[1].SetActive(false);
    }

    private void HandleRun()
    {
        Vector3 movement = new Vector3(0f, 0f, forwardZ) * runningSpeed * Time.deltaTime;
        playerY.transform.Translate(movement, Space.Self);
        floraY.transform.Translate(movement, Space.Self);
    }
}