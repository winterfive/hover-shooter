using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GenericManager<UIManager> {
    

    private void UpdateHealthUI(int i)
    {
        // TODO update health display in HMD
    }


    private void UpdateScoreUI(int j)
    {
        // TOD update score display in HMD
    }


    private void OnEnable()
    {
        GameManager.OnUpdatePlayerHealth += UpdateHealthUI;
        GameManager.OnUpdatePlayerScore += UpdateScoreUI;
    }


    private void OnDisable()
    {
        GameManager.OnUpdatePlayerHealth -= UpdateHealthUI;
        GameManager.OnUpdatePlayerScore -= UpdateScoreUI;
    }

    
}
