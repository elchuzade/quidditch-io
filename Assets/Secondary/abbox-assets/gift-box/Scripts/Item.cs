using UnityEngine;
using static GlobalVariables;

[CreateAssetMenu(fileName = "New Spinner Item", menuName = "Spinner Item")]
public class Item : ScriptableObject
{
    public GameObject itemPrefab;
    [Header("* Must Be Unique")]
    public Skill itemName;
    public int chanceCount;
}
