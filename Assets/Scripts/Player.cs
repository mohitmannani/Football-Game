using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private ball ballAttachedToPlayer;
    private float timeShot = -1f;
    public const int ANIMATION_LAYER_SHOOT = 1;
    private int myScore, otherScore;


    public ball BallAttachedToPlayer { get => ballAttachedToPlayer; set => ballAttachedToPlayer = value; }

    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame

    void Update()
    {


        if (starterAssetsInputs.shoot)
        {

            Debug.Log("Shoot!");


            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0f);
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f);
        }
        if (timeShot > 0)
        {
            // shoot ball
            if (ballAttachedToPlayer != null && Time.time - timeShot > 0.2)
            {
                ballAttachedToPlayer.StickToPlayer = false;
                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootdirection = transform.forward;
                shootdirection.y += 0.2f;
                rigidbody.AddForce(transform.forward * 20f, ForceMode.Impulse);
                ballAttachedToPlayer = null;
            }
            // finish kicking animation
            if (Time.time - timeShot > 0.5)
            {
                timeShot = -1f;
            }
        }
        else
        {
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(ANIMATION_LAYER_SHOOT), 0f, Time.deltaTime * 10f));
        }


    }


    public void IncreaseMyScore()
    {
        myScore++; 
        UpdateScore();
    }

    public void IncreaseOtherSore()
    {
        otherScore ++ ;

        UpdateScore();
    }

    private void UpdateScore()
    {
        textScore.text = "Score: " + myScore.ToString() + "-" + otherScore.ToString();
     }
                                                                                          

}



  