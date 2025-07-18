using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    PlayerInput playerInput;
    Rigidbody rb;
    Animator animator;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
        animator.SetFloat("Move", playerInput.move);
      
    }

    void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDistance); //MovePosition은 전역 위치를 사용한다. 
    }

    void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(0, turn, 0f);

    }
}
