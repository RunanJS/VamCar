using UnityEngine;

public class BumperBase : MonoBehaviour, PartBase<GameObject>
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual bool SkillEffect(GameObject target)
    {
        return true;
    }

    public void OnTriggerEnter(Collider other)
    {
        SkillEffect(other.gameObject);   
    }
}
