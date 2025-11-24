using System.Collections;
using UnityEngine;

public enum AmmoType
{
    SIMPLE,
    FIRE
}


public class Projectile : MonoBehaviour
{
    [SerializeField] AmmoType _type;
    public AmmoType type { get { return _type; } }

    [SerializeField] float _speed;

    [SerializeField] int _damage;

    [SerializeField] float _timeToDeactivate;

    float _moveDirX = 0;
    float _moveDirY = 0;

    GameObject _parent;

    private void LateUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(_moveDirX, _moveDirY);
    }

    public void MoveDirection(float a, float b)
    {
        _moveDirX = _speed * a;
        _moveDirY = _speed * b;
        transform.eulerAngles = new Vector2(a, 0);
    }

    public void SetParentObj(GameObject obj)
    {
        _parent = obj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine("TimeToDie");

        _parent.GetComponent<PoolingObjs>().DeactivateObj(this.gameObject);
    }

    public IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(_timeToDeactivate);

        StopCoroutine("TimeToDie");

        _parent.GetComponent<PoolingObjs>().DeactivateObj(this.gameObject);
    }
}
