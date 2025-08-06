using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    PlayerInput input;
    Animator animator;


    void Start()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);

    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.fire)
        {
            gun.Fire();
        }

        else if (input.reload) 
        {
            if (gun.Reload())
            {
                animator.SetTrigger("Reload");
            }
        }
    }
}
