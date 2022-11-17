using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
   public bool ON = false;   

    public void onClicked()
    {
        ON = true;
    }

    public void offClicked()
    {
        ON = false;
    }


}
