using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void testUpdate(int value)
    {
        this.text.text = value.ToString();
    }
}
