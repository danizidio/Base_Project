using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using TMPro;

public class WorldPlayerPosition : MonoBehaviour
{
    public enum SceneLocations
    {
        POINT_A
       ,POINT_B
       ,POINT_C
       ,POINT_D
       ,POINT_E
       ,POINT_F
    }

    [SerializeField] SO_ScenesCollection _sOScenes;

    [SerializeField] SceneLocations _sceneLocation;
    public SceneLocations sceneLocation { get { return _sceneLocation; } }

    [SerializeField] SceneLocations _locationToGo;

    [SerializeField] SceneAsset _scene;

    GameObject _player;

    private void Start()
    {
        GetComponentInChildren<TMP_Text>().text = _sceneLocation.ToString();
    }

    void SpawnPlayer()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Portal");


        foreach(var p in g)
        {
            if(p.GetComponent<WorldPlayerPosition>().sceneLocation == _locationToGo)
            {
                GameManager.OnSpawnPlayer?.Invoke(new Vector2(p.transform.position.x + 3, p.transform.position.y));

                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.GetComponent<PlayerBehaviour>();

        if (p == null) return;

        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(LoadingNextScene(_scene));
    }

    public IEnumerator LoadingNextScene(SceneAsset sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName.name);

        while (!loading.isDone)
        {
            yield return null;
        }
        if(loading.isDone)
        {
            SpawnPlayer();
        }
    }


}
