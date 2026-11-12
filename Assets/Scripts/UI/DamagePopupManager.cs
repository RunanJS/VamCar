using System.Globalization;
using UnityEngine;
using UnityEngine.Pool;

public class DamagePopupManager : MonoBehaviour
{
    static DamagePopupManager inst;

    [SerializeField] DamagePopup prefab;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color critColor = new Color(1f, 0.8f, 0f);

    ObjectPool<DamagePopup> pool;

    void Awake()
    {
        inst = this;
        pool = new ObjectPool<DamagePopup>(
            Create,
            p => p.gameObject.SetActive(true),
            p => p.gameObject.SetActive(false),
            p => Destroy(p.gameObject),
            defaultCapacity: 20, maxSize: 100);
    }

    DamagePopup Create()
    {
        var p = Instantiate(prefab, transform);
        p.Init(pool.Release);
        return p;
    }

    public static void Show(Vector3 pos, float damage, bool crit = false)
    {
        string str = damage.ToString("0.#", CultureInfo.InvariantCulture);
        Show(pos, str, crit ? inst.critColor : inst.normalColor, crit ? 1.5f : 1f);
    }

    public static void Show(Vector3 pos, string text, Color color, float scale = 1f)
    {
        if (inst == null) return;

        pos += new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));

        var p = inst.pool.Get();
        p.Show(pos, text, color, scale);
    }
}