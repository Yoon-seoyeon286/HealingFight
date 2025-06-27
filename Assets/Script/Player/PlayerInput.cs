using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; //앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Horizontal"; //좌우 움직임을 위한 입력축 이름       

    public float move { get; private set; } //감지된 움직임 입력값
    public float rotate{ get; private set; } //감지된 회전 입력값


    void Update()
    {
        move = Input.GetAxis(moveAxisName); //move에 관한 입력을 감지
        rotate = Input.GetAxis(rotateAxisName); 
    }
}
