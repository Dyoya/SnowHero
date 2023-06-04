using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    public int clearStage; // 현재 클리어된 스테이지의 수 
    public GameObject stageNumObject; // 스테이지 버튼의 부모
    public int maxStarsPerStage = 3; // 최대 별 갯수
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
        //PlayerPrefs.SetInt("stage", 0); // 기본으로 0으로 설정되어있음 , 스테이지 5까지 열고싶으면 ("stage",4) 를 주시면됩니다. 
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
                Debug.Log("별추가");
            }
        }

    }
}
