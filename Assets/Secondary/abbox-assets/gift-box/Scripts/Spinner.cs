using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static GlobalVariables;

public class Spinner : MonoBehaviour
{
    [SerializeField] List<Item> itemObjects = new List<Item>();
    [SerializeField] GameObject items;

    [SerializeField] int stopSpinnerTimer;

    bool spinning;

    Skill pickedSkill;

    [SerializeField] GiftWindow giftWindow;

    #region Private Methods
    void CreateItems()
    {
        for (int i = 0; i < itemObjects.Count; i++)
        {
            GameObject item = Instantiate(itemObjects[i].itemPrefab, items.transform.position, Quaternion.identity);
            item.transform.SetParent(items.transform);
            item.transform.localScale = Vector3.one;
            item.name = itemObjects[i].itemName.ToString();
            item.SetActive(false);
        }
    }

    void ShowGiftPrefab(Skill gift)
    {
        for (int i = 0; i < items.transform.childCount; i++)
        {
            if (items.transform.GetChild(i).gameObject.name == gift.ToString())
            {
                items.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                items.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Public Methods
    public void InitializeSpinner()
    {
        CreateItems();
        StartSpinning();
    }

    public Skill GetGiftName()
    {
        return pickedSkill;
    }

    public void StartSpinning()
    {
        spinning = true;

        StartCoroutine(TurnSpinner());
        StartCoroutine(StopSpinner());
    }

    public void ResetSpinner()
    {
        for (int i = 0; i < items.transform.childCount; i++)
        {
            Destroy(items.transform.GetChild(i).gameObject);
        }
    }

    // Generate gift when spinning is complete
    public void GenerateGift()
    {
        List<Skill> allGifts = new List<Skill>();

        // Add all gifts to the pool and withdraw one, based on the chance
        for (int i = 0; i < itemObjects.Count; i++)
        {
            for (int j = 0; j < itemObjects[i].chanceCount; j++)
            {
                allGifts.Add(itemObjects[i].itemName);
            }
        }

        int randomGiftIndex = Random.Range(0, allGifts.Count);
        pickedSkill = allGifts[randomGiftIndex];
        
        ShowGiftPrefab(pickedSkill);
        giftWindow.SetBallSkill(pickedSkill);
    }
    #endregion

    #region Coroutines
    IEnumerator StopSpinner()
    {
        yield return new WaitForSeconds(stopSpinnerTimer);

        GenerateGift();
        spinning = false;
    }

    IEnumerator TurnSpinner()
    {
        int i = Random.Range(0, items.transform.childCount);

        if (spinning)
        {
            items.transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);

        if (spinning)
        {
            items.transform.GetChild(i).gameObject.SetActive(false);
            StartCoroutine(TurnSpinner());
        }
    }
    #endregion
}
