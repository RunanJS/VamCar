using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicStorage : StorageBase
{
    [SerializeField] Transform layout;
    [SerializeField] GameObject slotPrefab;
    public int size = 16;
    public List<Button> btns = new List<Button>();

    void Start()
    {
        inventory = new Inventory(size);
        btns = new List<Button>();
        for (int i = 0; i < size; i++)
        {
            btns.Add(Instantiate(slotPrefab,layout).GetComponent<Button>());
        }

        for (int i = 0;i < btns.Count; i++)
        {
            int index = i;
            btns[index].onClick.AddListener(() =>
            {
                HandedItem(index);
                Debug.Log(index);
            });
        }
    }

    public override void HandedItem(int index)
    {
        base.HandedItem(index);
        btns[index].GetComponentInChildren<TextMeshProUGUI>().text =
            inventory[index].amount + "";
    }
}