using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUpUI : MonoBehaviour
{
    [SerializeField] private GameObject[] textSequence = new GameObject[3];

    [SerializeField] private EventSenderSO onReadyUpFinished;

    private void Awake()
    {
        foreach(var item in textSequence)
        {
            item.SetActive(false);
        }
    }

    public void StartReadyUp()
    {
        StartCoroutine(ReadyUpSequence());
    }

    private IEnumerator ReadyUpSequence()
    {
        WaitForSeconds wait1 = new WaitForSeconds(0.8f);
        WaitForSeconds wait2 = new WaitForSeconds(0.2f);
        for(int i = 0; i < textSequence.Length - 1; i++)
        {
            textSequence[i].SetActive(true);
            yield return wait1;
            textSequence[i].SetActive(false);
            yield return wait2;
        }
        textSequence[textSequence.Length - 1].SetActive(true);
        onReadyUpFinished.Invoke();
        yield return wait1;
        textSequence[textSequence.Length - 1].SetActive(false);
    }
}
