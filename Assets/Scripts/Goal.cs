using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Player scriptPlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerEnter(Collider other)
    {

        if (name.Equals("GoalDetector"))
        {
            scriptPlayer.IncreaseMyScore();
        }
        else
        {
            scriptPlayer.IncreaseOtherSore();
        }
     

    }
}
