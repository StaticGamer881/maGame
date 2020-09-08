using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MaGame {
    public class playerMovement : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float m_speed = 1000f;
        [SerializeField] private float m_sprintModifier = 2f;
        [SerializeField] Camera thisCamera;
        [SerializeField] private Transform groundDectector;
        [SerializeField] private Transform weaponParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject cameraParent;
        [SerializeField] private float jumpForce = 50;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float sensitivityX = 1f;
        private int currentHealth;
        private float RunCounter;
        private float walkCounter;
       // private Manager manager;
        private bool isGrounded;
        private bool escPressed = false;
        private bool isJumping;
        private float
            m_hMove,
            m_vMove;
        private bool
            sprint,
            jump;
        private Rigidbody rb;
        private float ogFOV;
        private float sprintFOVModifier = 1.5f;
        private bool isSprinting;
        private Vector3 weaponParentOGPosition;
        [SerializeField] LayerMask ground;

        void Start()
        {
           // manager = GameObject.Find("manager").GetComponent<manager>();
            currentHealth = maxHealth;

            cameraParent.SetActive(photonView.IsMine);
            if (!photonView.IsMine) gameObject.layer = 10;

            ogFOV = thisCamera.fieldOfView;
            rb = GetComponent<Rigidbody>();
            weaponParentOGPosition = weaponParent.localPosition;
        }

        void FixedUpdate()
        {
            if (!photonView.IsMine) return;

            isGrounded = Physics.Raycast(groundDectector.position, Vector3.down, 1.5f, ground);
            isJumping = jump && isGrounded;
            isSprinting = sprint && m_vMove > 0 && isGrounded && !isJumping;
            Vector3 _jump = Vector3.up * jumpForce;

            if (isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            

            Vector3 direction = new Vector3(m_vMove, 0, m_hMove);
            direction.Normalize();

            float adjSpeed = m_speed;

            if (isSprinting) { adjSpeed *= m_sprintModifier; }
            Vector3 velocity = transform.TransformDirection(direction) * adjSpeed * Time.deltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;

            if (isSprinting) { thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, sprintFOVModifier * ogFOV, 8f * Time.deltaTime); }
            else { thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, ogFOV, 8f * Time.deltaTime); }

            if (Input.GetMouseButton(1))
            {
                thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, ogFOV / 4, 8f * Time.deltaTime);
            }
        }

        void Update()
        {
            if (!photonView.IsMine) return;

            if (Input.GetKeyDown(KeyCode.Escape) && escPressed) { escPressed = false; }
            else if (Input.GetKeyDown(KeyCode.Escape) && !escPressed) { escPressed = true; }
            m_hMove = Input.GetAxisRaw("Horizontal") * -1;
            m_vMove = Input.GetAxisRaw("Vertical");

            sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            jump = Input.GetKey(KeyCode.Space);

            if (!escPressed) 
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                mouse();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            
            // StartCoroutine(groundedBoolRoutine());

            // head movement ra ree!!
            
            if (isSprinting)
            {
                HeadBob(RunCounter, 0.1f, 0.1f);
                RunCounter += Time.deltaTime * 12;
            }
            
            else if (!isGrounded || isJumping)
            {
                HeadBob(1, 0, 0);
            }
            else if (m_hMove != 0 || m_vMove != 0)
            {
                HeadBob(walkCounter, 0.05f, 0.05f);
                walkCounter += Time.deltaTime * 9;
            }
        }
        void mouse()
        {
            float mouse = Input.GetAxis("Mouse X");
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += mouse * sensitivityX;
            transform.localEulerAngles = newRotation;
        }

        void HeadBob(float z, float xIntensity, float yIntensity)
        {
            if (!Input.GetMouseButton(1))
            {
                weaponParent.localPosition = weaponParentOGPosition + new Vector3(Mathf.Cos(z) * xIntensity, Mathf.Sin(z * 2) * yIntensity, 0);
            }
            else
            {
                weaponParent.localPosition = weaponParentOGPosition;
            }
        }

        public void takeDamage(int damage)
        {
            if (photonView.IsMine)
            {
                currentHealth -= damage;
                Debug.Log(currentHealth);
                if(currentHealth <= 0)
                {
                    // player kill
                }
            }
        }

    }
}