using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public void ContinueButton()
    {
        GameManager.Instance.HandleStateChange(GameManager.GameState.Playing);
    }

    public void MainMenuButton()
    {
        GameManager.Instance.HandleStateChange(GameManager.GameState.Menu);
    }
}
