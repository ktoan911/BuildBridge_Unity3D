using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Characters
{
    [SerializeField] private FloatingJoystick joystick;

    [SerializeField] private CharacterController characterController;


    private float horizontal;
    private float vertical;

    public void SetupJoyStick(FloatingJoystick floatingJoystick)
    {
        joystick = floatingJoystick;
    }

    //=====StartGame=====
    private void SetGravity()
    {
        characterController.Move(Vector3.down * gravity * Time.deltaTime);
    }

    //==Run on Update()===
    protected override void OnStartGame()
    {
        base.OnStartGame();

        GetInput();

        IsInputHandler();

        if (CheckStairForward())
        {
            MoveOnStair();
        }

        if (base.IsMoving)
        {
            Moving();
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        SetGravity();
    }

    //===PlayerMovement=====

    private void GetInput()
    {
        if (joystick == null) return;
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
    }
    private void IsInputHandler()
    {
        if(Input.GetMouseButton(0))
        {
            base.IsMoving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            base.IsMoving = false; 
        }
    }
    private void Moving()
    {
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            characterController.Move(direction * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            animator.SetBool("IsRun",true);
        }

    }

    //===BulidStair====
    private void MoveOnStair()
    {
        if (vertical > 0.001f)
        {
            if (CheckMatSameStairForward())
            {
                return;
            }
            else
            {
                if (listBrickCharacter.Count <= 0)
                {
                    base.IsMoving = false;
                    return;
                }
                else 
                {
                    hitStair.collider.GetComponent<StairCtrl>().SetStairColor((int)colorType);
                    hitStair.collider.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(listBrickCharacter[0]);
                    listBrickCharacter.RemoveAt(0);
                }
            }
        }

    }

    //===return colortype===
    public override ColorType OnWin()
    {
        return this.colorType;
    }

}
