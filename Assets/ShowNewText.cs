using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowNewText : MonoBehaviour
{
    Animator anim;
    Text text;
    public static ShowNewText showNewText;
    void Start()
    {
        showNewText = this;
        text = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }

    public void NewText(string newText)
    {
        text.text = newText;
        anim.SetTrigger("FadeOut");
    }
}
