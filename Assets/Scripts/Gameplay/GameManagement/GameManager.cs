using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject asteroidProto;
    [SerializeField] GameObject asteroidParent;
    [SerializeField] List<GameObject> bonusesProto;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    static GameManager _instance;
    private void Start()
    {
        StartCoroutine(WaitAndSpawnAsteroid());
        StartCoroutine(WaitAndSpawnBonus());
        _instance = this;
    }

    IEnumerator WaitAndSpawnAsteroid()
    {
        var wait = new WaitForSeconds(1.0f);
        while (true)
        {
            yield return wait;
            if (GameContext.Instance.asteroids.Count < GameParams.ASTEROIDS_MAX)
            {
                Instantiate(asteroidProto, GetRandomPosition(), Quaternion.identity, asteroidParent.transform);
            }
        }
    }
    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-35, 35), 0, Random.Range(-25, 25));
    }

    IEnumerator WaitAndSpawnBonus()
    {
        var wait = new WaitForSeconds(1.0f);
        while (true)
        {
            yield return wait;
            if (GameContext.Instance.bonuses.FindAll(b => b.enable).Count < GameParams.BONUSES_MAX)
            {
                var bonus = ObjectsPool.Instance.GetBonus();
                bonus.transform.parent = null;
                bonus.transform.position = GetRandomPosition();
               // Instantiate(bonusesProto[Random.Range(0, bonusesProto.Count)], GetRandomPosition(), Quaternion.identity, asteroidParent.transform);
            }
        }
    }


    public void InitializeRespawn(SpaceShip client)
    {
        StartCoroutine(_InitializeRespawn(client));
    }

    IEnumerator _InitializeRespawn(SpaceShip client)
    {
        yield return new WaitForSeconds(2.0f);

        client.transform.position = GetRandomPosition();
        client.OnSpawn();
        client.gameObject.SetActive(true);
    }
}
