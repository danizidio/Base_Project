using UnityEngine;

[CreateAssetMenu()]
public class SO_AbilitiesLearned : ScriptableObject
{
    [SerializeField] bool _doubleJump;
    public bool doubleJump { get { return _doubleJump; } }

    [SerializeField] bool _explosiveWeapon;
    public bool explosiveWeapon { get { return _explosiveWeapon; } }
}
