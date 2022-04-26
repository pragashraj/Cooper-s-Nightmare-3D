using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectivePanel;
    [SerializeField] private Text objectivePanelText;
    [SerializeField] private GameObject[] objIndicators;

    public enum LastCompletedObjective
    {
        NONE, OBJ1, OBJ2, OBJ3, OBJ4, OBJ5, OBJ6, OBJ7, OBJ8
    }

    private string[] objectives = new string[] {
        "Objective 1 : Meet Flora",
        "Objective 2 : Leave City & Direct to mountains",
        "Objective 3 : Return to city & find flora",
        "Objective 4 : Go to near manhantton shop",
        "Objective 5 : Lead flora back to safe house",
        "Objective 6 : Find a vehicle for escaping",
        "Objective 7 : Back to safe house",
        "Objective 8 : Get flora out of city"
    };

    public LastCompletedObjective lastCompletedObjective = LastCompletedObjective.NONE;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetLastCompletedObjective(LastCompletedObjective obj)
    {
        lastCompletedObjective = obj;
    }

    public LastCompletedObjective GetLastCompletedObjective()
    {
        return lastCompletedObjective;
    }

    public void SetObjectivePanel(string text, int nextObjInx)
    {
        objectivePanel.SetActive(true);
        objectivePanelText.text = text;

        objIndicators[nextObjInx - 1].SetActive(false);

        if (nextObjInx < objectives.Length)
        {
        	objIndicators[nextObjInx].SetActive(true);
	}

        StartCoroutine(HandleObjectveFlow(nextObjInx));
    }

    IEnumerator HandleObjectveFlow(int nextObjInx)
    {
        if (nextObjInx < objectives.Length)
        {
            yield return new WaitForSeconds(4f);
            objectivePanelText.text = objectives[nextObjInx];
        }

        yield return new WaitForSeconds(4f);
        objectivePanelText.text = "";
        objectivePanel.SetActive(false);
    }


    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    public void PlayThemeForAliens()
    {
        PlayAudio("ThemeForAliens");
    }
}