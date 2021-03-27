using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _lifetimer = 1f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 60f);
        _lifetimer -= Time.deltaTime;
        if (_lifetimer <= 0)
        {
            Destroy(gameObject, 0.3f);
        }
    }
}
