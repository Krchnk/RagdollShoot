using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _fullBody;
    [SerializeField] private Transform _handHelper;
    [SerializeField] private GameObject _trigger;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private ParticleSystem _flash;
    [Header("Choose Mode")] public GameMode GameMode;
    private NavMeshAgent _agent;
    private Rigidbody _rigidBody;
    private bool _isRunning;
    private bool _isWalkingMode;
    private float _xRotation = 0f;
    private float _fireRate;
    private float _timer = 0f;
    private EnemyController _enemyController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _isWalkingMode = true;
        _rigidBody = GetComponent<Rigidbody>();
        _enemyController = _enemy.GetComponent<EnemyController>();
        _agent.speed = GameMode.playerSpeed;
        _fireRate = GameMode.shootingSpeed;
        _enemyController.BulletForce = GameMode.bulletForce;
    }

    void Update()
    {
        if (_isWalkingMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    _agent.SetDestination(hit.point);
                }
            }
            float speed = _agent.velocity.magnitude;
            _animator.SetFloat("movement", speed);
            _hand.SetActive(false);
            _fullBody.SetActive(true);
        }
        if (!_isWalkingMode)
        {
            _hand.SetActive(true);
            _fullBody.SetActive(false);
            RaycastHit hit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out hit))
            {
                Debug.DrawLine(cameraRay.origin, hit.point);
                _handHelper.transform.LookAt(hit.point);
                _bulletSpawn.transform.LookAt(hit.point);

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bulletObject = Instantiate(_bulletPref, _bulletSpawn.position, _bulletSpawn.rotation);
                    _flash.Play();
                }

                if (Input.GetMouseButton(0))
                {
                    _timer += Time.deltaTime;
                    if (_timer > _fireRate)
                    {
                        GameObject bulletObject = Instantiate(_bulletPref, _bulletSpawn.position, _bulletSpawn.rotation);
                        _timer = 0f;
                        _flash.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isWalkingMode = false;
        _animator.SetFloat("movement", 0f);
        _agent.enabled = false;
        gameObject.transform.position = new Vector3(other.transform.position.x,
                                                    transform.position.y,
                                                    other.transform.position.z);
        transform.LookAt(_enemy.transform, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        Camera.main.transform.localPosition = new Vector3(0f, 1f, 0f);
        _trigger.SetActive(false);
    }
}
