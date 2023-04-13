using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : Characters
{
    private NavMeshAgent agent;

    private Vector3 finalPosition;

    private bool isFoundBrick;
    public bool IsFoundBrick { get => isFoundBrick; set => isFoundBrick = value; }

    [SerializeField] private LayerMask layerBrickGround;

    private IState<EnemyCtrl> currentState;

    private List<GameObject> Stairs;


    private GameObject currentPlatform;


    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

    }
    protected override void OnInit()
    {
        base.OnInit();

        Stairs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Stair"));

        IsFoundBrick = false;

        ChangeState(new IdleState());

    }

    //Hàm chạy trong Update
    protected override void OnStartGame()
    {


        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Debug.Log(colorType + currentState.ToString());
        base.OnStartGame();


    }


    //===SeekBrick=====
    public void MoveToBrick()
    {
        
        if(!IsFoundBrick)
        {
            this.SeekForbrick();
        }

        if(Vector3.Distance(transform.position,finalPosition) < 0.3f)
        {
 
            IsFoundBrick = false;
        }
    }

    private void SeekForbrick()
    {
        Vector3 scaleBox = new Vector3(100, 5, 100);
        Collider[] hitColliders = Physics.OverlapBox(this.transform.position, scaleBox / 2, Quaternion.identity, layerBrickGround);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<BrickCtrl>().colorType == this.colorType)
            {
                finalPosition = hitColliders[i].transform.position;
                if (finalPosition == hitColliders[i].transform.position) IsFoundBrick = true;
                else IsFoundBrick = false;
                agent.SetDestination(finalPosition);
            }
        }
    }

    //====BuildStair=====
    public void SetGoalPickStair()
    {

        if (CheckOnGroundActive())
        {
            currentPlatform = hitGround.collider.gameObject;
            if(listBrickCharacter.Count >= 4)
            {
                GotoStair();
            }
        }
        BuildStair();

        agent.SetDestination(finalPosition);


    }

    private void BuildStair()
    {
        if (CheckMatSameStairForward() && CheckStairForward())
        {
            if (Vector3.Distance(transform.position, finalPosition) < 1.5f)
            {
                finalPosition = PLatformManager.Instance.listPlatform[PLatformManager.Instance.listPlatform.Count - 1].transform.position;
                
            }
        }
        else if (CheckStairForward())
        {
            if (listBrickCharacter.Count <= 0 && CheckOnStair())
            {
                if (Vector3.Distance(finalPosition, currentPlatform.transform.position) > 0.1f)
                {
                    IsFoundBrick = false;
                    finalPosition = currentPlatform.transform.position;
                    agent.velocity = Vector3.zero;
                }

            }
            else if (listBrickCharacter.Count > 0)
            {
                hitStair.collider.GetComponent<StairCtrl>().SetStairColor((int)colorType);
                hitStair.collider.GetComponent<MeshRenderer>().enabled = true;
                Destroy(listBrickCharacter[0]);
                listBrickCharacter.RemoveAt(0);
                finalPosition = PLatformManager.Instance.listPlatform[PLatformManager.Instance.listPlatform.Count - 1].transform.position;
            }
        }
    }

    private void GotoStair()
    {
        if ((int)colorType + Tmp >= Stairs.Count)
        {
            finalPosition = PLatformManager.Instance.listPlatform[PLatformManager.Instance.listPlatform.Count - 1].transform.position;
            return;
        }
            
        finalPosition = Stairs[(int)colorType + Tmp].transform.position;      
        agent.SetDestination(finalPosition);
    }

    //====ChangeState======
    public void ChangeState(IState<EnemyCtrl> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    //====Return colortype=====
    public override ColorType OnWin()
    {
        Debug.Log(colorType);
        return this.colorType;
    }

}

