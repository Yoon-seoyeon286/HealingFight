using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooter : MonoBehaviour
{
    public Transform gunPivot;
    public Transform rightHandMount;
    public Transform leftHandMount;
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

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);


        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

        gunPivot.position = animator.GetIKPosition(AvatarIKGoal.LeftHand);
        gunPivot.rotation = animator.GetIKRotation(AvatarIKGoal.LeftHand);

    }
}
