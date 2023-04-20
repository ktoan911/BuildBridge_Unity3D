using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private List<ColorType> listColor;

    private List<GameObject> listEnemy;
    [SerializeField] private Player player;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;

    private bool isPause;
    public bool IsPause { get => isPause; set => isPause = value; }
    public Player Player { get => player; }

    private void Awake()
    {
        listColor = new List<ColorType>();

        listEnemy = new List<GameObject>();



        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        UIManager.Ins.OpenUI<MainMenu>();
    }
    private void Start()
    {
        PauseGame();
        AddColorList();
        SpawnBot();
    }

    private void Update()
    {
        if (!IsPause)
        {
            ResumeGame(); // tiếp tục hoạt động của game           
        }
    }

    private void AddColorList()
    {
        listColor.Add((ColorType)0);
        listColor.Add((ColorType)1);
        listColor.Add((ColorType)2);
        listColor = listColor.OrderBy(x => Random.Range(-10,10)).ToList();
    }

    private void SpawnBot()
    {
        for (int i = 0; i < listColor.Count - 1; i++)
        {
            enemy = Instantiate(enemyPrefab, new Vector3(0, 5, 0), this.transform.rotation);
            listEnemy.Add(enemy);
        }
    }

    public void PauseGame()
    {
        IsPause = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }




    //private IEnumerator StartDelay()
    //{
    //    Time.timeScale = 0f; // tạm dừng toàn bộ hoạt động của game
    //    if(!UIManager.Instance.startGame.activeSelf)
    //    {
    //        yield return new WaitForSecondsRealtime(3f); // chờ 3 giây thực sự
    //        Time.timeScale = 1f; // tiếp tục hoạt động của game
    //    }
    //}

}
