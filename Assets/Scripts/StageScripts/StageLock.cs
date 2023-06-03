using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    public int clearStage; // ���� Ŭ����� ���������� �� 
    public GameObject stageNumObject; // �������� ��ư�� �θ�
    public int maxStarsPerStage = 3; // �ִ� �� ����
    public Sprite unLockImage;


    private void Start()
    {
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("stage_star_0", 0);
        //PlayerPrefs.SetInt("stage_star_1", 0);
        //PlayerPrefs.SetInt("stage_star_2", 0);
        //PlayerPrefs.SetInt("stage_star_3", 0);
        //PlayerPrefs.SetInt("stage_star_4", 0);
        //PlayerPrefs.SetInt("stage", 0); // �⺻���� 0���� �����Ǿ����� , �������� 5���� ��������� ("stage",4) �� �ֽø�˴ϴ�. 
        clearStage = PlayerPrefs.GetInt("stage");

        
        Debug.Log(clearStage.ToString());

        for (int i = 0; i <= clearStage; i++) 
        {
            stages[i].interactable = true;
            stages[i].GetComponent<Image>().sprite = unLockImage;
            int stars = PlayerPrefs.GetInt("stage_star_" + i.ToString());
            stars = Mathf.Min(stars, maxStarsPerStage);
            for (int j = 0; j < stars; j++)
            {
                stages[i].transform.GetChild(j).gameObject.SetActive(true);
                Debug.Log("���߰�");
            }
        }

    }
}
