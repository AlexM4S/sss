using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public enum SIDE { Left,Mid,Right}

public enum HitX {  Left, Mid, Right, None }

public enum HitY { Up, Mid, Down, None }

public enum HitZ { Forward, Mid, Backward, None }

public class Character : MonoBehaviour
{

    public SIDE m_Side = SIDE.Mid;
    
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue;
    private CharacterController m_char;
    private Animator m_Animator;
    private float x;
    public float SpeedDodge;
    public float JumpPower = 7f;
    private float ForwardSpeed = 4f;
    private float y;
    public bool InJump;
    public bool InRoll;
    private float ColHeight;
    private float ColCenterY;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    private SIDE LastSide;

    public Text Score;
    public Text LastScore;
    public Text HighScore;
   


    void Start()
    {
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        m_Animator = GetComponent<Animator>();
        transform.position = Vector3.zero;

        LastScore.text = PlayerPrefs.GetInt("lastScore", 0).ToString();
        HighScore.text = PlayerPrefs.GetInt("hScore", 0).ToString();

    }

    
        
   
    void Update()
    {

        if(GameManager.sharedInstance.isGameOver)
        {
            return;
        }


        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        int number = Mathf.FloorToInt(transform.position.z);
        Score.text = number.ToString();

        PlayerPrefs.SetInt("lastScore", number);

        if (number > PlayerPrefs.GetInt("hScore", 0))
        {
            PlayerPrefs.SetInt("hScore", number);
            HighScore.text = number.ToString();
        }



        if (SwipeLeft&& !InRoll)
        {
            if(m_Side == SIDE.Mid)
            {
                LastSide = m_Side;
                m_Side = SIDE.Left;
                m_Animator.Play("SALTOizd");
                ResetCollision();
            }

            else if(m_Side == SIDE.Right)
            {
                LastSide = m_Side;
                m_Side = SIDE.Mid;
                m_Animator.Play("SALTOizd");
                ResetCollision();
            }
            else
            {
                LastSide = m_Side;
                m_Animator.Play("stumble");
                ResetCollision();
            }
            
        }
        else if (SwipeRight&& !InRoll)
        
        {
            if (m_Side == SIDE.Mid)
            {
                LastSide = m_Side;
                m_Side = SIDE.Right;
                m_Animator.Play("SALTOder");
                ResetCollision();
            }

            else if (m_Side == SIDE.Left)
            {
                LastSide = m_Side;
                m_Side = SIDE.Mid;
                m_Animator.Play("SALTOder");
                ResetCollision();
            }
            else
            {
                LastSide = m_Side;
                m_Animator.Play("stumble");
                ResetCollision();
            }
        }

        Vector3 moveVector = new Vector3(x - transform.position.x, y*Time.deltaTime, ForwardSpeed*Time.deltaTime);
        x = Mathf.Lerp(x, (int)m_Side, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);
        Jump();
        Roll();
        
    }

    public void Jump()
    {
        if (m_char.isGrounded)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("falling"))
            {
                m_Animator.Play("landing2");
                InJump = false;
            }
            if (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("jumping", 0.1f);
                InJump = true;
            }
        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime;
            if (m_char.velocity.y < -0.1f)
            
            m_Animator.Play("falling");
        }
    }

    internal float RollCounter;

    public void Roll()
    {
        RollCounter -= Time.deltaTime;
        if(RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0, ColCenterY, 0);
            m_char.height = ColHeight;
            InRoll = false;
        }
        if (SwipeDown)
        {
            RollCounter = 0.5f;
            y -= 10f;
            m_char.center = new Vector3(0, ColCenterY/5f, 0);
            m_char.height = ColHeight/5f;
            m_Animator.CrossFadeInFixedTime("roll", 0.1f);
            InRoll = true;
            InJump = false;
        }
    }

    public void OnCharacterColliderHit(Collider col)
    {
        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);

        if (hitZ == HitZ.Mid)
        {
            if (hitX == HitX.Right)
            {
                m_Side = LastSide;
                m_Animator.Play("stumble");
                ResetCollision();
            }
            else if (hitX == HitX.Left)
            {
                m_Side = LastSide;
                m_Animator.Play("stumble");
                ResetCollision();
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Mid)
            {
                if (hitX == HitX.Mid)
                {
                    PlayDeadAnim();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Mid)
            {
                if (hitX == HitX.Right)
                {
                    PlayDeadAnim();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Mid)
            {
                if (hitX == HitX.Left)
                {
                    PlayDeadAnim();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Up)
            {
                if (hitX == HitX.Mid)
                {
                    PlayDeadAnim();
                }
            }
        }

        
    }


    void PlayDeadAnim()
    {

        m_Animator.Play("death");
        ResetCollision();
        GameManager.sharedInstance.GoToGameOver();
    }

    private void ResetCollision()
    {
        print(hitX.ToString() + hitY.ToString() + hitZ.ToString());
        hitX = HitX.None;
        hitY = HitY.None;
        hitZ = HitZ.None;
    }

    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x + max_x) / 2f - col_bounds.min.x;
        HitX hit;
        if (average > col_bounds.size.x - 0.33f)
            hit = HitX.Right;
        else if (average < 0.33f)
            hit = HitX.Left;
        else
            hit = HitX.Mid;
        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y)/char_bounds.size.y;
        HitY hit;
        if (average < 0.33f)
            hit = HitY.Down;
        else if (average < 0.66f)
            hit = HitY.Mid;
        else
            hit = HitY.Up;
        return hit;
    }

    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - char_bounds.min.z) / char_bounds.size.z;
        HitZ hit;
        if (average < 0.33f)
            hit = HitZ.Backward;
        else if (average < 0.66f)
            hit = HitZ.Mid;
        else
            hit = HitZ.Forward;
        return hit;
    }
}
