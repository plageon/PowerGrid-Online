using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCoal : MonoBehaviour
{

    public Text targetText;

    public Slider targetSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.targetText.text = "Coal: "+this.targetSlider.value.ToString();
    }
}
