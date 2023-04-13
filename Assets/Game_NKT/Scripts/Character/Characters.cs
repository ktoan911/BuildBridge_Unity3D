using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Characters : MonoBehaviour
{
    public static List<ColorType> colorList= new List<ColorType>();
    [SerializeField] protected GameObject brickCharacterPrefab;
    [SerializeField] protected Transform brickCharacterCloneGroup;



    [SerializeField] private LayerMask layerStair;
    [SerializeField] private LayerMask layerWallStair;
    [SerializeField] private LayerMask layerGround;

    protected GameObject brickCharacter;
    public List<GameObject> listBrickCharacter;

    [SerializeField] private PlatformCtrl platformCtrl;

    [SerializeField] protected float gravity;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;

    public Animator animator;

    [SerializeField] protected ColorData colorData;
    public ColorType colorType;

    protected RaycastHit hitStair;
    protected RaycastHit hitGround;

    protected float brickThickness;

    private bool isMoving;
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private bool isSpawnBrick;

    protected int Tmp = 0;

    protected virtual void Awake() 
    {
        listBrickCharacter = new List<GameObject>();
    }
    private void Start()
    {
       OnInit();        
    }
    private void Update()
    {
        OnStartGame();       
    }

    //====Start====
    protected virtual void OnInit()
    {
     
        IsMoving = false;
        isSpawnBrick= true;
        speed = 7f;
        rotationSpeed = 20f;

        brickThickness = 0.3f;

        gravity = 20f;

        do
        {
            colorType = (ColorType)Random.Range(0, 3);
        } while (colorList.Contains(colorType));
        colorList.Add(colorType);
        
        ChangeMaterial(this.gameObject, colorData.GetColor((int)colorType));

    }

    //=====Update========
    protected virtual void OnStartGame()
    {
        SetActivePlatform();
        OpenWallStair();
        UpdateBrickPosition();
        RefillBrickGrround(this.colorType);
    }


    //==== Character Material ==========
    protected void ChangeMaterial(GameObject obj, Material newMaterial)
    {
        if (obj == null) return;

        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }

        // Đệ quy để áp dụng material mới cho tất cả các object con
        foreach (Transform child in obj.transform)
        {
            ChangeMaterial(child.gameObject, newMaterial);
        }
    }

    //====Brick Character=========
    protected void UpdateBrickPosition()
    {
        Vector3 brickPos = this.transform.position - this.transform.forward;
        Quaternion brickRot = this.transform.rotation;
        for (int i = 0; i < listBrickCharacter.Count; i++)
        {
            brickPos.y = this.transform.position.y + brickThickness * i;
            listBrickCharacter[i].transform.position = brickPos;
            listBrickCharacter[i].transform.rotation = brickRot;
        }
    }

    protected void SpawnBrick()
    {
        Vector3 spawnPos = this.transform.position;
        brickCharacter = Instantiate(brickCharacterPrefab, spawnPos, this.transform.rotation);
        brickCharacter.GetComponent<BrickCtrl>().SetBrickColor((int)colorType);
        listBrickCharacter.Add(brickCharacter);
        brickCharacter.transform.SetParent(brickCharacterCloneGroup);
    }


    //===CheckStair===
    public bool CheckOnStair()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1.7f, layerStair))
        {
            return true;
        }
        return false;
    }

    protected bool CheckStairForward()
    {
        if (Physics.Raycast(this.transform.position + Vector3.forward + Vector3.up, Vector3.down, out hitStair, 1.5f, layerStair))
        {
            return true;
        }

        return false;
    }

    protected bool CheckMatSameStairForward()
    {
        if (CheckStairForward())
        {
            if (hitStair.collider.GetComponent<StairCtrl>().colorType == this.colorType)
            {
                return true;
            }
        }
        return false;

    }
    protected void OpenWallStair()
    {
        RaycastHit hitWallStair;
        if (CheckOnStair() && Physics.Raycast(this.transform.position + Vector3.up, this.transform.forward, out hitWallStair, 1.5f, layerWallStair))
        {
            hitWallStair.collider.gameObject.SetActive(false);
            Tmp += 3;
            isSpawnBrick= true;
        }
    }

    //======Platform========
    protected void SetActivePlatform()
    {
        RaycastHit hitGround;

        if (Physics.Raycast(transform.position, Vector3.down, out hitGround, 1.5f, layerGround))
        {
            platformCtrl = hitGround.collider.GetComponent<PlatformCtrl>(); 
            if (platformCtrl != null && isSpawnBrick)
            {
                platformCtrl.SpawnBrickByColorType(this.colorType);
                isSpawnBrick = false;
            }
        }

    }
    protected void RefillBrickGrround(ColorType colorType)
    {
        if (CheckOnStair())
        {
            platformCtrl.RefillBrick(colorType);
        }
    }
    public bool CheckOnGroundActive()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out hitGround, 0.2f, layerGround))
        {
            return true;
        }
        return false;
    }



    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BrickGround") && other.GetComponent<BrickCtrl>().colorType == this.colorType)
        {
            other.gameObject.SetActive(false);

            SpawnBrick();
        }


    }

    public virtual ColorType OnWin()
    {
        return colorType;
    }


}
