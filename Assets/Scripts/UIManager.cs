using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GenericManager<UIManager> {
    

    private void UpdateHealthUI(int i)
    {
        //Debug.Log("Player health is: " + i);
    }


    private void UpdateScoreUI(int j)
    {
        //Debug.Log("The score is: " + j);
    }


    private void OnEnable()
    {
        GameManager.OnUpdatePlayerHealth += UpdateHealthUI;
        GameManager.OnUpdateScore += UpdateScoreUI;
    }


    private void OnDisable()
    {
        GameManager.OnUpdatePlayerHealth -= UpdateHealthUI;
        GameManager.OnUpdateScore -= UpdateScoreUI;
    }

    
}
