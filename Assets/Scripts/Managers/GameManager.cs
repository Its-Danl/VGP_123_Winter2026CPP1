using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    //observer pattern
    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    #endregion

    #region Life Management
    
    private int _lives = 3; // internal value
    private int maxLives = 5;

    public int Lives //C# property accessors
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                GameOver();
                return;
            }

            if (_lives > value)
            {
                Respawn();
            }
            _lives = value;

            if (value > maxLives)
            {
                _lives = maxLives;
            }

            Debug.Log("Life value changed to: " + _lives);
        }
    }
    #endregion


    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    private Vector3 currentCheckpoint;

    void Update()
    {

        //string currentSceneName = SceneManager.GetActiveScene().name;
        //string sceneToLoad = currentSceneName == "Title" ? "Game" : "Title";

        //SceneManager.LoadScene(sceneToLoad);
    }

    public void SpawnPlayer(Vector3 spawnPos)
    {
        _playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        UpdateCheckpoint(spawnPos);

        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint) => currentCheckpoint = newCheckpoint;

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }

    private void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint;
    }
}
