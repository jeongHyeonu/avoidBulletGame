using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<Cannon> Cannon;
    [SerializeField] public GameObject GameStartUI;
    [SerializeField] public GameObject GameOverUI;
    [SerializeField] public GameObject moveUI;
    [SerializeField] public TextMeshProUGUI score;

    public float cannonSpawnTime;
    public bool isGameStarted;
    private float cannonCooltime;
    private int currentScore;


    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        Time.timeScale = 0f;
        GameStartUI.SetActive(true);

        instance = this;
    }

    public void GameStart()
    {
        GameStartUI.SetActive(false);
        Time.timeScale = 1f;
        score.gameObject.SetActive(true);
        moveUI.SetActive(true);
        currentScore = 0;
        isGameStarted = true;
        cannonSpawnTime = 1f;
        cannonCooltime = cannonSpawnTime;
    }

    public void GameOver()
    {
        isGameStarted = false;
        GameOverUI.SetActive(true);
        moveUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted == false) return;
        cannonCooltime -= Time.deltaTime;
        if (cannonCooltime <= 0 && Player.Instance.isDied==false)
        {
            int randCannonNum = Random.Range(0, Cannon.Count);
            Cannon cannon = Cannon[randCannonNum].GetComponent<Cannon>();
            cannon.ShootBullet(1f);
            cannonCooltime = cannonSpawnTime;
            score.text = "Score : " + ++currentScore;
        }
        cannonSpawnTime -= Time.deltaTime * 0.01f;
    }
}
