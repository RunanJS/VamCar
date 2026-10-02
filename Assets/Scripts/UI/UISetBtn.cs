using UnityEngine;
using UnityEngine.UI;

public class UISetBtn : MonoBehaviour
{
    Button btn;
    [SerializeField] EngineUI ui;
    [SerializeField] IConnecter target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            ui.SetInventory(target);
        });
    }
}
