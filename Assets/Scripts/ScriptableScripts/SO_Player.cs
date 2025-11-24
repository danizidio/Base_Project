using UnityEngine;

[CreateAssetMenu()]
public class SO_Player : ScriptableObject
{
    [SerializeField] float _maxHealth;
    public float maxHealth { get { return _maxHealth; } }

    [SerializeField] float _maxMana;
    public float maxMana { get { return _maxMana; } }

    [SerializeField] float _walkSpeed;
    public float walkSpeed { get { return _walkSpeed; } }

    [SerializeField] float _jumpForce;
    public float jumpForce { get { return _jumpForce; } }

}
