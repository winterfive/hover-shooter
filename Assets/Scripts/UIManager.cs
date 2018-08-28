using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GenericManager<UIManager> {
    

    private void UpdateHealthUI()
    {
        // TODO update health display in game
    }


    private void OnEnable()
    {
        PlayerManager.OnUpdatePlayerHealth += UpdateHealthUI;
    }


    private void OnDisable()
    {
        PlayerManager.OnUpdatePlayerHealth -= UpdateHealthUI;
    }

    
}
