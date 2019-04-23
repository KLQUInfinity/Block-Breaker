using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region singleton Variable
    public static LevelManager Instance;                //singleton variable
    #endregion

    [Header("Prefabs")]
    #region prefabs
    [SerializeField] private GameObject brick;              //prefab for brick
    [SerializeField] private GameObject paddle;             //prefab for the paddle and ball

    public GameObject brickParticles;     //prefab for the brick destroing particles
    public GameObject deathParticles;     //prefab for the paddle destroing particles
    #endregion

    [Header("Info")]
    #region info
    [SerializeField] private int rows;          //number of rows of bricks to create
    [SerializeField] private int columns;       //number of columns of bricks to create
    [SerializeField] private float xStart, xShift, yStart, yShift;      //the cordinate system for postioning bricks
    [SerializeField] private float ballInitialVelocity;                 //the init velocity of ball
    [SerializeField] private float paddleSpeed;                         //the speed of the paddle
    [SerializeField] private float minPaddle_xPos, maxPaddle_xPos;      //the bound of paddle move

    [HideInInspector] public int BrickNum = 0;                 //number of bricks that created from method CreateRandomLevel()
    [HideInInspector] public bool BallInPlay = false;          //flag for ball to check it move or not (play or pause)
    [HideInInspector] public int Lifes = 3;                    //number of lifes

    private float xBall = 1;                                   //the direction of the ball for start shoot
    private bool isPause = false;                              //flag for the game in pause state or not
    private Vector3 lastSpeed;
    #endregion

    [Header("used objects")]
    #region used objects
    [SerializeField] private Slider moveSlider;                 //the UI slider that control paddle movement
    [SerializeField] private GameObject startBtn;               //the start button

    [HideInInspector] public GameObject PlayerPaddle;           //the created paddle gameobject
    [HideInInspector] public GameObject PlayerBall;             //the created ball gameobject
    #endregion

    private void Awake()
    {
        Instance = this;

        CreateRandomLevel();
        Setup();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    /// <summary>
    /// Create the random bricks in the level
    /// </summary>
    private void CreateRandomLevel()
    {
        //create the random bricks
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    BrickNum++;
                    Vector3 brickPos = new Vector3((xStart + (j * xShift)), (yStart - (i * yShift)), 0);
                    Instantiate(brick, brickPos, Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// create the paddle and ball, 
    /// setup the playing states
    /// </summary>
    private void Setup()
    {
        //create paddle
        PlayerPaddle = Instantiate(paddle, paddle.transform.position, Quaternion.identity);
        PlayerBall = PlayerPaddle.transform.GetChild(0).gameObject;
        BallInPlay = false;

        //setup the playing states
        moveSlider.value = 0;
    }

    /// <summary>
    /// Start playing
    /// </summary>
    public void StartPlay()
    {
        if (!BallInPlay)
        {
            PlayerBall.transform.parent = null;
            BallInPlay = true;
            PlayerBall.GetComponent<Rigidbody>().isKinematic = false;

            PlayerBall.GetComponent<Rigidbody>().AddForce(new Vector3(ballInitialVelocity * xBall, ballInitialVelocity, 0f));
        }
    }

    /// <summary>
    /// Change the direction of ball for start shoot
    /// </summary>
    public void Change_xBall()
    {
        xBall = (moveSlider.value >= 0) ? 1 : -1;
    }

    /// <summary>
    /// Change the x position for the paddle 
    /// </summary>
    public void ChangePaddle_xPos()
    {
        if (PlayerPaddle != null)
        {
            float xPaddlePos = Mathf.Clamp(moveSlider.value * paddleSpeed, minPaddle_xPos, maxPaddle_xPos);
            PlayerPaddle.transform.position = new Vector3
                (
                xPaddlePos,
                PlayerPaddle.transform.position.y,
                0f
                );
        }
    }

    /// <summary>
    /// Pause and resume the Game state
    /// </summary>
    public void PauseGameToggle()
    {
        isPause = !isPause;

        if (isPause)
        {
            lastSpeed = PlayerBall.GetComponent<Rigidbody>().velocity;
        }
        PlayerBall.GetComponent<Rigidbody>().velocity = (isPause) ? Vector3.zero : lastSpeed;
    }

    public void DestroyBrick()
    {
        BrickNum--;

    }
}
