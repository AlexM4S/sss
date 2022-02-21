using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float ForwardSpeed = 7f;
    private float y;
    public bool InJump;
    public bool InRoll;
    private float ColHeight;
    private float ColCenterY;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    private SIDE LastSide;
   


    void Start()
    {
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        m_Animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);


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
            RollCounter = 0.6f;
            y -= 10f;
            m_char.center = new Vector3(0, ColCenterY/3f, 0);
            m_char.height = ColHeight/3f;
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
                    m_Animator.Play("death");
                    ResetCollision();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Mid)
            {
                if (hitX == HitX.Right)
                {
                    m_Animator.Play("death");
                    ResetCollision();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Mid)
            {
                if (hitX == HitX.Left)
                {
                    m_Animator.Play("death");
                    ResetCollision();
                }
            }
        }

        if (hitZ == HitZ.Forward)
        {
            if (hitY == HitY.Up)
            {
                if (hitX == HitX.Mid)
                {
                    m_Animator.Play("death");
                    ResetCollision();


                }
            }
        }


       

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
