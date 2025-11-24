using UnityEngine;

[CreateAssetMenu()]
public class SO_Enemies : ScriptableObject
{
    [SerializeField] float _maxHealth;
    public float maxHealth { get { return _maxHealth; } }

    [SerializeField] float _walkSpeed;
    public float walkSpeed { get { return _walkSpeed; } }

}
