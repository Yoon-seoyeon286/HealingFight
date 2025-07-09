using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; //앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Horizontal"; //좌우 움직임을 위한 입력축 이름       
    public VariableJoystick joystick;
    public FixedJoystick rotateJoystick;

    public float move { get; private set; } //감지된 움직임 입력값
    public float rotate{ get; private set; } //감지된 회전 입력값


    void Update()
    {
        /* if (GameManager.instance != null && GameManager.instance.isGameOver)
         {

         }*/

        move = joystick.Vertical;
        rotate = rotateJoystick.Horizontal;

    }
}
