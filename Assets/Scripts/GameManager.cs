using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }
    #endregion
    public bool IsGameGoing { get; private set; }

    private int score;
    public int Score
    {
        get { return score; }
        set 
        { 
            score = value;
            scoreText.text = "Score: " + score;
        }
    }

    [Header("Timing Values")]
    [SerializeField] private float commonStartDelay = 2f;
    [SerializeField, Tooltip("Start Enemy Spawn Delay")] private float SESD = 2f;
    [SerializeField] private float enemyDelayShortage = 10f;
    [SerializeField] private float weaponSpawnDelay = 10f;
    [SerializeField] private float bonusSpawnDelay = 27f;

    [Header("Factories")]
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private WeaponFactory weaponFactory;
    [SerializeField] private BonusFactory bonusFactory;
    [SerializeField] private ZoneFactory zoneFactory;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private GameObject endUI;
    private float delayTime;
    void Start()
    {
        delayTime = SESD;
        IsGameGoing = true;
        zoneFactory.GetNewZones();
        StartCoroutine(RepeatableEnemySpawn());
        StartCoroutine(RepeatableWeaponSpawn());
        StartCoroutine(RepeatableBonusSpawn());
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }
    public void EndGame()
    {
        IsGameGoing = false;
        endUI.SetActive(true);
        if (Score > ScoreRecord.GetRecord())
        {
            ScoreRecord.SetRecord(Score);
            recordText.text = $"Score: {Score}\nNew record!";
        }
        else recordText.text = $"Score: {Score}\nRecord: {ScoreRecord.GetRecord()}";
    }
    #region COROUTINES
    IEnumerator RepeatableEnemySpawn()
    {
        yield return new WaitForSeconds(commonStartDelay);
        StartCoroutine(InterTimeShortage());
        while (IsGameGoing)
        {
            enemyFactory.GetNewEnemy();
            yield return new WaitForSeconds(delayTime);
        }
    }
    IEnumerator RepeatableBonusSpawn()
    {
        yield return new WaitForSeconds(commonStartDelay);
        while (IsGameGoing)
        {
            bonusFactory.GetNewBonus();
            yield return new WaitForSeconds(bonusSpawnDelay);
        }
    }
    IEnumerator RepeatableWeaponSpawn()
    {
        yield return new WaitForSeconds(commonStartDelay);
        while (IsGameGoing)
        {
            weaponFactory.GetNewWeapon();
            yield return new WaitForSeconds(weaponSpawnDelay);
        }
    }
    IEnumerator InterTimeShortage()
    {
        while(delayTime > 0.5f)
        {
            yield return new WaitForSeconds(enemyDelayShortage);
            delayTime -= 0.1f;
        }
    }
    #endregion
}
