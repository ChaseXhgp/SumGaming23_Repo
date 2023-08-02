using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public Light Flashlight_Light;
    public GameObject Monster;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            CollectPickup(other.gameObject);

            //Destroy the pickup
            Destroy(other.gameObject);
        }
    }

     
    IEnumerator FreezeMonsterRoutine()
    {
        // Code before the yield statement
        Debug.Log("Monster has been frozen.");
          UnityEngine.AI.NavMeshAgent monsterNavAgent = Monster.GetComponent<UnityEngine.AI.NavMeshAgent>();
          monsterNavAgent.speed = 0.0f;
        

        // Yield to wait for a number of seconds
        yield return new WaitForSeconds(7.0f);

        // Code after the yield statement
        Debug.Log("Powerup End");
       
    }

    IEnumerator BoostSpeedRoutine()
    {
        // Code before the yield statement
        Debug.Log("Speed has been boosted..");
        walkSpeed +=5;
        runSpeed +=10;


        // Yield to wait for a number of seconds
        yield return new WaitForSeconds(4.0f);

        // Code after the yield statement
        Debug.Log("Powerup End");
        walkSpeed -=5;
        runSpeed -=10;
       
    }

    IEnumerator BoostFlashlightRoutine()
    {
        // Code before the yield statement
        Debug.Log("Flashlight range has been boosted.");
        Flashlight_Light.range += 40;


        // Yield to wait for a number of seconds
        yield return new WaitForSeconds(5.0f);

        // Code after the yield statement
        Debug.Log("Powerup End");
        Flashlight_Light.range -= 40;
       
    }
    
    private void CollectPickup(GameObject pickup)
    {
        Pickup ability = pickup.GetComponent<Pickup>();


        //Check the pickup type
        switch (ability.pickupAbility)
        {
            case PickupAbility.FreezeMonster:
                //Add Buff
                Debug.Log("Freeze Monster");
                StartCoroutine(FreezeMonsterRoutine());
                
                break;
            case PickupAbility.BoostSpeed:
                //Add Buff
                Debug.Log("Boost Speed");
                StartCoroutine(BoostSpeedRoutine());
                break;
            case PickupAbility.BoostFlashlight:
                Debug.Log("Boost Flashlight");
                StartCoroutine(BoostFlashlightRoutine());
                //Add Buff
                break;
            default: break;

        }
    }
}