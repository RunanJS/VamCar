using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartManager : MonoBehaviour
{
    public static PartManager Inst;
    [SerializeField] PopupUI popup;
    [SerializeField] PartBase[] partPrefabs;
    [SerializeField] Button[] slotBtn = new Button[6];
    [SerializeField] CarManager car;
    [SerializeField] PartBase selectedPart;

    public static IReadOnlyCollection<PartBase> GetParts => Inst.partPrefabs;

    public void StartSelect(PartBase part)
    {
        selectedPart = part;
        popup.Open();
        Time.timeScale = 0;
    }

    public void SelectSlot(int index)
    {
        car.SetPart(index, selectedPart);
        popup.Close();
        Time.timeScale = 1;
    }

    public static PartBase GetPart(string id)
    {
        foreach (var item in Inst.partPrefabs)
        {
            if(item.id == id)
            {
                return item;
            }
        }
        Debug.Log("그런거없다");
        return null;
    }

    void Awake()
    {
        if (Inst == null)
            Inst = this;
        else
            Destroy(Inst);
    }

    public void OnEnable()
    {
        for (int i = 0; i < slotBtn.Length; i++)
        {
            int index = i;
            slotBtn[index].onClick.AddListener(() => SelectSlot(index));
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < slotBtn.Length; i++)
        {
            int index = i;
            slotBtn[index].onClick.RemoveAllListeners();
        }
    }
}
