using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject storyTextpanel;
    [SerializeField] private Text storyText;

    public void SetStoryText(string msg)
    {
        storyTextpanel.SetActive(true);
        storyText.text = msg;
    }

    public void SetStoryTextPanel(string[] messages)
    {
        StartCoroutine(HandleStoryTextMessages(messages));
    }

    IEnumerator HandleStoryTextMessages(string[] messages)
    {
        for (int i = 0; i < messages.Length; i++)
        {
            string msg = messages[i];
            SetStoryText(msg);

            yield return new WaitForSeconds(3f);
        }

        yield return new WaitForSeconds(3f);
        storyTextpanel.SetActive(false);
    }
}