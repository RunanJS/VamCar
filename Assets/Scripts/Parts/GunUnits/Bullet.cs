using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = -1;
    [SerializeField] private float power;
    [SerializeField] private Transform target;

    public void SetBullet(float _speed, float _power, Transform _target)
    {
        speed = _speed;
        power = _power;
        target = _target;
        transform.LookAt(target);
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if (speed <= 0) return;

        transform.Translate(Vector3.forward * Time.deltaTime
            * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(target.tag) && 
            other.TryGetComponent<Unit>(out var unit))
        {
            unit.OnDamaged(power);
            Destroy(gameObject);
        }
    }
}