using UnityEngine;

public class LevelModifier : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private ObjectPool standPool;
    public static event System.Action speedUp;

    private void Awake()
    {
        enemyPool = enemyPool.GetComponent<ObjectPool>();
        standPool = standPool.GetComponent<ObjectPool>();
    }

    private void OnEnable()
    {
        GameManager.gameBegin += StartGame;
    }

    private void StartGame(int l)
    {
        level = l;
        PlaceEnemies();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement p))
        {
            ChangeLevel();
            speedUp?.Invoke();
        }
    }

    private void ChangeLevel()
    {
        if (level < 10)
        {
            level++;
        }

        ClearLevel();
        PlaceEnemies();
    }

    public void PlaceEnemies()
    {
        for (int i = 1; i < level * 2; i++)
        {
            GameObject e = enemyPool.Get();
            e.transform.position = new Vector3(0, 0f, 200 / (level * 2) * i);
            e.gameObject.SetActive(true);

            if (Random.value > 0.7)
            {
                GameObject s = standPool.Get();
                s.transform.position = new Vector3(0, 0.5f, 200 / (level * 2) * i);
                e.transform.position += new Vector3(0, 0.5f, 0);
                s.gameObject.SetActive(true);
            }
        }
    }

    private void ClearLevel()
    {
        foreach (Transform t in enemyPool.transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                t.GetComponent<Enemy>().ResetEnemy();
            }
        }

        foreach (Transform t in standPool.transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                t.GetComponent<Stand>().ResetStand();
            }
        }
    }

    private void OnDisable()
    {
        GameManager.gameBegin -= StartGame;
    }
}
