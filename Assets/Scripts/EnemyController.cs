using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _resetButtom;
    private Animator _animator;
    private Rigidbody _rigidbody;
    public float BulletForce;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.LookAt(_player.transform, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            _animator.enabled = false;
            _rigidbody.AddForce(other.transform.position * BulletForce, ForceMode.Impulse);
            _resetButtom.SetActive(true);
        }
    }
}
