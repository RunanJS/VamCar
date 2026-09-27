using UnityEngine;

public class SawBumper : MonoBehaviour
{
    [SerializeField] GameObject[] obj;
    [SerializeField] float power = 1;

    void Update()
    {
        float speed = 500 * Time.deltaTime;
        obj[0].transform.Rotate(0, speed, 0);
        obj[1].transform.Rotate(0, -speed, 0);
    }

    public void OnTriggerStay(Collider other)
    {

        Debug.Log(other.name);
        if (other.TryGetComponent<Unit>(out Unit target))
        {
            target.OnDamaged(power * Time.deltaTime);
        }
    }
}
