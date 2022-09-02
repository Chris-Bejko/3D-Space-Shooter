using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameUI GameUI;

    public MainMenu MainMenuUI;

    [System.Serializable]
    public class UiIdToCanvas
    {
        public UIScreenId screenId;
        public GameObject canvas;
    }

    public List<UiIdToCanvas> UiMapList = new();

    private Dictionary<UIScreenId, GameObject> UiMap = new();

    private List<GameObject> screenStack = new();


    private void Awake()
    {
        foreach (var entry in UiMapList)
        {
            UiMap.Add(entry.screenId, entry.canvas);
            screenStack.Add(entry.canvas);
        }
    }

    public void HandleScreenOpen(UIScreenId screen)
    {

        Debug.Log("UIEvent on screen: " + screen);

        if (!UiMap.TryGetValue(screen, out GameObject value))
        {
            Debug.LogError("There is no Game Object that corresponds to the screen id: " + screen.ToString());
            return;
        }

        foreach (var entry in screenStack)
        {
            if (entry.activeInHierarchy)
                entry.SetActive(false);
        }

        if (screenStack.Contains(value))
        {
            value.SetActive(true);
        }
        else
        {
            var temp = Instantiate(value);
            screenStack.Add(temp);
        }

    }
}

public enum UIScreenId
{
    PauseMenu,
    LostMenu,
    MainMenu,
    InGameMenu,

}