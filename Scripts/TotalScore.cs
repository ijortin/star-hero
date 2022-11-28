using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
	
    void Start()
    {
        scoreText.text = PointCounter.instance.Score() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
