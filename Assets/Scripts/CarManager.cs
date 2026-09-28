using UnityEngine;

public class CarManager : MonoBehaviour
{
    /*
    public PartBase<GameObject>[] top;
    public PartBase<GameObject>[] bottom;
    public PartBase<GameObject>[] left;
    public PartBase<GameObject>[] right;
    public PartBase<GameObject>[] front;
    public PartBase<GameObject>[] back;
    public PartBase<GameObject>[] inside;
    */

    [SerializeField] Transform[] slotPoint = new Transform[6];

    // 상하전후좌우
    PartBase[] slot = new PartBase[6];

    public void SetSlot(int index, PartBase part)
    {
        slot[index] = part;
        slot[index].gameObject.transform.position = slotPoint[index].position;
        slot[index].gameObject.transform.rotation = slotPoint[index].rotation;
        //return false;
    }

    public float EnginePower
    {
        get { return 1; }
    }
}
