using UnityEngine;
using UnityEngine.VFX;
using SaveLoadPlayerPrefs;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    [SerializeField] VisualEffect _leaves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        PlayerSavePoint(SceneManager.GetActiveScene().name, this.transform.position);
    }

    public void PlayerSavePoint(string scene, Vector2 pos)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveString(SaveStrings.SAVEPOINT_SCENE, scene);

        s.PlayerSaveFloat(SaveStrings.SAVEPOINT_POSITION_X, pos.x);
        s.PlayerSaveFloat(SaveStrings.SAVEPOINT_POSITION_Y, pos.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _leaves.GetComponent<VisualEffect>().SetFloat("TurbulenceIntensity", 1.59f);
        _leaves.GetComponent<VisualEffect>().SetFloat("TurbulenceDrag", 1.59f);
        _leaves.GetComponent<VisualEffect>().SetVector3("SetVelocityMinimum", new Vector3(-.13f, .5f, 0));
        _leaves.GetComponent<VisualEffect>().SetVector3("SetVelocityMaximum", new Vector3(.13f, 4, 0));
        _leaves.GetComponent<VisualEffect>().playRate = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _leaves.GetComponent<VisualEffect>().SetFloat("TurbulenceIntensity",0);
        _leaves.GetComponent<VisualEffect>().SetFloat("TurbulenceDrag",0);
        _leaves.GetComponent<VisualEffect>().SetVector3("SetVelocityMinimum", Vector3.zero);
        _leaves.GetComponent<VisualEffect>().SetVector3("SetVelocityMaximum", new Vector3(0, 0, 0));
        _leaves.GetComponent<VisualEffect>().playRate = .1f;
    }
}
