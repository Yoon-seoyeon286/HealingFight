using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooter : MonoBehaviour
{
    public Transform gunPivot;
    public Transform rightHandMount;
    public GameObject gun;

    PlayerInput playerInput;
    Animator animator;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent < Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

    }
}
