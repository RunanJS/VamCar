using UnityEngine;

public class ExpObject : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    MeshRenderer sprite;
    [Header("자석")]
    [SerializeField] float magnetRadius = 3f;
    [SerializeField] float magnetSpeed = 1f;

    void Awake()
    {
        sprite = GetComponent<MeshRenderer>();
        Refresh();
    }

    public int GetExp()
    {
        Destroy(gameObject);
        return amount;
    }

    public void Refresh()
    {
        Color res = Color.white;
        if (amount <= 1) res = Color.red;
        else if (amount == 2) res = new Color32(255, 127, 0, 255);
        else if (amount == 4) res = Color.yellow;
        else if (amount == 8) res = Color.green;
        else if (amount == 16) res = Color.blue;
        else if (amount == 32) res = new Color32(75, 0, 130, 255);
        else if (amount >= 64) res = new Color32(143, 0, 255, 255);

        sprite.material.color = res;

        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);

        // 자석
        var car = CarManager.Inst;
        if (car == null) return;

        Vector3 pos = transform.position;
        Vector3 target = car.transform.position;
        target.y = pos.y;                          // 높이는 유지

        float sqrDist = (target - pos).sqrMagnitude;
        if (sqrDist > magnetRadius * magnetRadius) return;   // 범위 밖

        transform.position = Vector3.MoveTowards(pos, target, magnetSpeed * Time.deltaTime);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<ExpObject>(out var exp))
        {
            if (exp.amount != amount) return;
            exp.amount += GetExp();
            exp.Refresh();
            Refresh();
        }

        var car = other.GetComponentInParent<CarManager>();
        if (car == null) return;

        car.AddExp(GetExp());
        Refresh();
        /*
        if (other.TryGetComponent<CarManager>(out var car))
        {
            Debug.Log("닿음");
            car.AddExp(GetExp());
            Refresh();
        }*/
    }
}
