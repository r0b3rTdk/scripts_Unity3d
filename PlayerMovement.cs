using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemente : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;
    [SerializeField]private Transform foot;
    [SerializeField]private LayerMask colisionLayer;
    [Header("Variables")]
    public float speed = 5f;
    private bool isGround;
    private float yForce;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Mover();
        Pular();
    }
    public void Mover()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;	

        controller.Move(movimento * Time.deltaTime * speed);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }
        animator.SetBool("Mover", movimento != Vector3.zero);   
        isGround = Physics.CheckSphere(foot.position, 0.3f, colisionLayer); 
        animator.SetBool("isGround", isGround);
    }
    public void Pular()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            yForce = 5f;
            animator.SetTrigger("Jump");
        }
        if (yForce > -9.81f)
        {
            yForce += -9.81f * Time.deltaTime;
        }
        
        controller.Move(new Vector3(0, yForce, 0) * Time.deltaTime);

    }
}
