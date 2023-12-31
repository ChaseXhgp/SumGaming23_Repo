using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Serialized Field allows you to edit private variables within the unity editor. 
    [SerializeField, Tooltip("THe highest speed of the player.")]
    private float _moveSpeed = 7.0f;

    [SerializeField, Tooltip("The force which the player jumps at.")]
    private float jumpForce = 10.0f;

    [SerializeField, Tooltip("The force that the player is brought down to the ground at.")]
    private float _gravity = 10.0f;

    [SerializeField, Tooltip("The Character Controller component on this.")]
    private CharacterController _pController;

    // Current Move Direction
    private Vector3 _moveDirection;

    public int pickUpCount;

    [SerializeField]
    private TextMeshProUGUI pickupText;

    // Start is called before the first frame update
    void Start()
    {
        _pController = GetComponent<CharacterController>();
        pickUpCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // Collect player input
        float _xInput = Input.GetAxis("Horizontal"); //Stores horizontal player input.
        float _zInput = Input.GetAxis("Vertical"); //Stores vertical player input.
        // Apply player movement based on inputs

        Vector3 movement = new Vector3(_xInput, 0, _zInput);

        movement = transform.TransformDirection(movement) * _moveSpeed; // vector3 --> local movements
        if (_pController.isGrounded) // If the player is on the ground
        {
            _moveDirection = movement;
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = jumpForce;
            }
        } else
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        _pController.Move(_moveDirection * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other) // Is called when the player collides with a trigger collider 
    {
        /*swtich (other.gameObject.tag) 
        {
            case "Pickup":
                other.gameObject.SetActive(false);
                pickUpCount += 1;
                break;

            case "Enemy":
                gameObject.SetActive(false);
                Debug.Log("YOU DIED!"); 
                break;

        } */


        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            pickUpCount += 1;
            Debug.Log("Score");
            pickupText.text = pickUpCount.ToString();
        }
    } 
}


