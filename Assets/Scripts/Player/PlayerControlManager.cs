using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public void MoveLeft(){
        PlayerControl.instance.MoveToLeft();
    }
    public void MoveRight(){
        PlayerControl.instance.MoveToRight();
    }
    public void Jump(){
        PlayerControl.instance.Jump();
    }

    public void Slide(){
        PlayerControl.instance.Slide();
    }
}
