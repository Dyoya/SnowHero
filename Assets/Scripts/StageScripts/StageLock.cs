using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    public int clearStage;
    public GameObject stageNumObject;

    private void Start()
    {
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        clearStage = PlayerPrefs.GetInt("stage");
        Debug.Log(clearStage.ToString());

        for (int i = clearStage + 1; i < stages.Length; i++)
            stages[i].interactable = false;
    }
}
