using UnityEngine;

public class A_Door : MonoBehaviour,IOpenable
{
    public Transform player;
    Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
       
        float PlayerToDistance = Vector3.Distance(transform.position, player.position);

        if (PlayerToDistance < 6f) {
            Debug.Log("OpenDoor 함수가 호출되었도다!");
            animator.SetTrigger("open");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
