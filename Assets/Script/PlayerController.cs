using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //struct stocke les directions et positions 
    [SerializeField]
    LifeSys lifeSystem;
    Vector3 direction;
    float vitesseRotation;
    float dureeRotation = 0.05f;
    [SerializeField]
    float jumpForce;
    float vitessePers;
    [SerializeField]
    public float vitesseMarche = 10;
    [SerializeField]
    public float vitesseSprint = 20;
    float playerSpeedRate;
    [SerializeField]
    Camera cam;
    [SerializeField]
    public Rigidbody rb;
    [SerializeField]
    Animator animator;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    public LayerMask enemyLayer;
    public Collider _col;

    [SerializeField] private CinemachineFreeLook cineCam;
    [SerializeField] private CinemachineVirtualCamera cineFpsCam;
    [SerializeField] private bool fpsMode;

    public float timeBetweenAttacks;
    float lastAttackTime;
    bool canAttack
    {
        get
        {
            if (Time.timeSinceLevelLoad - lastAttackTime < timeBetweenAttacks) return false;
            return true;
        }
    }
    bool alreadyAttacked = false;
    [SerializeField]
    Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public int health = 100;
    private bool sprintSpeed;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cineCam.enabled = !fpsMode; //true
        cineFpsCam.enabled = fpsMode; //false
        //lifeSystem.onDieDel = OnDie;
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            fpsMode = !fpsMode;
            Debug.Log("switch cam Mode " + fpsMode);
            cineCam.enabled = !fpsMode;
            cineFpsCam.enabled = fpsMode;
        }

        if (direction.magnitude > 0.1f)
        {
            MovePlayer();
            playerSpeedRate += Time.deltaTime * 1f;

        }
        else
        {
            playerSpeedRate -= Time.deltaTime * 2f;
        }
        playerSpeedRate = Mathf.Clamp(playerSpeedRate, 0, 1);

        //transform.LookAt(target, Vector3.up);
        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = direction.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vitessePers = vitesseSprint;
            SprintControl();
            animator.SetBool("Run", true);
        }
        else
        {
            vitessePers = vitesseMarche;
            animator.SetBool("Run", false);
        }

        animator.SetFloat("Walk", playerSpeedRate);

        bool _isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = true;
        }

        animator.SetBool("IsJumping", !_isGrounded);

        /*if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }*/
    }
    //fonction qui calcul les mouvements de la camera du joueur et du joueur 
    void MovePlayer()
    {
        float _targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float _anle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref vitesseRotation, dureeRotation);
        transform.rotation = Quaternion.Euler(0f, _anle, 0f);
        Vector3 _moveDir = Quaternion.Euler(0, _targetAngle, 0) * Vector3.forward;
        _moveDir = _moveDir.normalized;
        rb.MovePosition(transform.position + (_moveDir * (vitessePers * playerSpeedRate) * Time.deltaTime));
    }
    void SprintControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > vitessePers)
        {
            Vector3 limitedVel = flatVel.normalized * vitessePers;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, rb.velocity.z);
        }
    }
    //fonction qui permet d'établir si le joueur touche le sol grâce à un raycast
    bool IsGrounded()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
        return (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.4f, groundMask));
    }
}
