using UnityEngine;

public class CarManager : MonoBehaviour
{
    public PartBase<GameObject>[] top;
    public PartBase<GameObject>[] bottom;
    public PartBase<GameObject>[] left;
    public PartBase<GameObject>[] right;
    public PartBase<GameObject>[] front;
    public PartBase<GameObject>[] back;
    public PartBase<GameObject>[] inside;

    public float EnginePower
    {
        get { return 1; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
