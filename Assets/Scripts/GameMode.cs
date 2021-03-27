using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "GameMode", order = 51)]

public class GameMode : ScriptableObject
{
    [SerializeField]
    public float bulletForce;
    [SerializeField]
    public float playerSpeed;
    [SerializeField]
    public float shootingSpeed;
}
