using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModesWindow : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject pushMode;
    [SerializeField] GameObject targetMode;
    [SerializeField] GameObject pushModeSelected;
    [SerializeField] GameObject targetModeSelected;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        if (player.selectedMode == "push")
        {
            SelectPushMode();
        } else
        {
            SelectTargetMode();
        }
    }

    void SelectPushMode()
    {
        pushMode.transform.localScale = Vector3.one;
        pushModeSelected.SetActive(true);

        targetMode.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        targetModeSelected.SetActive(false);
    }

    void SelectTargetMode()
    {
        targetMode.transform.localScale = Vector3.one;
        targetModeSelected.SetActive(true);

        pushMode.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        pushModeSelected.SetActive(false);
    }

    public void ClickSelectPushMode()
    {
        if (player.selectedMode == "target")
        {
            SelectPushMode();
        }
        player.selectedMode = "push";
        player.SavePlayer();
    }

    public void ClickSelectTargetMode()
    {
        if (player.selectedMode == "push")
        {
            SelectTargetMode();
        }
        player.selectedMode = "target";
        player.SavePlayer();
    }

    public void ClickCloseGameModesWindow()
    {
        gameObject.SetActive(false);
    }
}
