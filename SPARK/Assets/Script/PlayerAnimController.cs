using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {
    [SerializeField]
    Animator anim;

    public void SetBool(string name,bool value) {
        anim.SetBool(name, value);
    }

    public void SetTrigger(string name) {
        anim.SetTrigger(name);
    }
}
