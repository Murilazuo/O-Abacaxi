using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHud : MonoBehaviour
{
    Text text;
    [TextArea][SerializeField] private string extraText = "";
    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        text.text = extraText + GameManager.coin.ToString();
    }
}
