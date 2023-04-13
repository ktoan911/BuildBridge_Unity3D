using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental;
using UnityEngine;

public class PlatformCtrl : MonoBehaviour
{
    private Vector3 firstBrickPos;

    private Vector3 platformScale;

    private int NumberBrickZ;

    private int NumberBrickX;

    private BrickCtrl brickClone;
    [SerializeField] private Transform brickCloneGroup;

    [SerializeField]private BrickCtrl brickPrefab;

    private List<BrickCtrl> listBrickGround;

    

    private float currentTime;
    private float timeDelay;

    private bool isRefill;
    public bool IsRefill { get => isRefill; set => isRefill = value; }


   public int color1 = 0;
    public int color2 = 0;
    public int color3 = 0;


    private void Awake()
    {
        listBrickGround = new List<BrickCtrl>();
    }
    private void Start()
    {
        this.OnInit();
    }

    //===SpawnBrick===

    private bool IsReSpawn()
    {
        color1 = 0;
        color2 = 0;
        color3 = 0;


        for (int i = 0; i < listBrickGround.Count; i++)
        {
            if (listBrickGround[i].GetComponent<BrickCtrl>().colorType != ColorType.blue)
            {
                color1++;
            }
            if (listBrickGround[i].GetComponent<BrickCtrl>().colorType != ColorType.red)
            {
                color2++;
            }
            if (listBrickGround[i].GetComponent<BrickCtrl>().colorType != ColorType.green)
            {
                color3++;
            }
        }

        if (color1 < 4 || color2 < 4 || color3 < 4)
        {
            for (int i = 0; i < listBrickGround.Count; i++)
            {
                Destroy(listBrickGround[0]);
                listBrickGround.RemoveAt(0);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnBrickOnGround()
    {
        do
        {
            for (int i = 3; i < NumberBrickZ - 3; i += 3)
            {
                for (int j = 3; j < NumberBrickX - 3; j += 3)
                {
                    int randomNum = Random.Range(0, 3);
                    Vector3 spawnPos = new Vector3(firstBrickPos.x + (float)i, firstBrickPos.y, firstBrickPos.z + (float)j);
                    brickClone = Instantiate(brickPrefab, spawnPos, this.transform.rotation);
                    brickClone.SetBrickColor(randomNum);

                    brickClone.transform.SetParent(brickCloneGroup);
                    listBrickGround.Add(brickClone);

                    brickClone.gameObject.SetActive(false);
                }
            }
        } while (IsReSpawn());
        for (int i = 0; i < listBrickGround.Count; i++)
        {
            brickClone.gameObject.SetActive(false);
        }
    }
    public void RefillBrick(ColorType colorType)
    {
        this.IsRefill= true;
        for(int i=0;i <listBrickGround.Count;i++)
        {
            if (!listBrickGround[i].IsActive() && listBrickGround[i].colorType == colorType)
            {
                ReActiveBrick(listBrickGround[i]);
            }          
            if(i > listBrickGround.Count - 1)
            {
                this.IsRefill= false;
            }
        }
    }
    private void ReActiveBrick(BrickCtrl brick)
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeDelay)
        {
            brick.gameObject.SetActive(true);
            currentTime = 0;
        }        
    }

    private void OnInit()
    {
        IsRefill = false;


        platformScale = this.transform.localScale;

        firstBrickPos.x = this.transform.position.x - (platformScale.x / 2f);
        firstBrickPos.z = this.transform.position.z - (platformScale.z / 2f);
        firstBrickPos.y = this.transform.position.y + 0.5f;

        NumberBrickZ = (int)platformScale.z;
        NumberBrickX = (int)platformScale.x;

        this.currentTime = 0;
        this.timeDelay = 0.7f;

        SpawnBrickOnGround();
    }

    internal void SpawnBrickByColorType(ColorType colorType)
    {
        for(int i = 0; i <listBrickGround.Count; i++)
        {
            if (listBrickGround[i].GetComponent<BrickCtrl>().colorType == colorType)
            {
                listBrickGround[i].gameObject.SetActive(true);
            }
        }
    }












    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(60, 5f, 60));
    //}

}

