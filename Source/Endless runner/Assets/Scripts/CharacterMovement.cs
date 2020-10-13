using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController _char_controller;

    Vector3 _move_vec;
    Vector3 _gravity;

    Animator _animator;
    public GameManager GM;

    public bool CanPlay;
    
    public int LifesCount;

    float _speed = 15;
    float _jumpSpeed = 10;

    int _laneNumber = 1;
    int _lanesCount = 2;

    public float FirstLanePos;
    public float LaneDistance;
    public float SideSpeed;

    bool _isRolling = false;

    Vector3 _ccCenterRoll = new Vector3(0, .6f, 0);
    Vector3 _ccCenterNorm;

    float _ccHeightRoll = .5f;
    float _ccHeightNorm;

    // Start is called before the first frame update
    void Start()
    {
        _char_controller = GetComponent<CharacterController>();

        _ccCenterNorm = _char_controller.center;
        _ccHeightNorm = _char_controller.height;

        _move_vec = new Vector3(1,0,0);
        _gravity = Vector3.zero;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GM.CanPlay)
        {
            return;
        }
        if (_char_controller.isGrounded)
        {
            _gravity = Vector3.zero;

            if (!_isRolling && CanPlay)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    _animator.SetTrigger("jumping");
                    _gravity.y = _jumpSpeed;
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    StartCoroutine(Rolling());
                }
            }
        }
        

        else
        {
            _gravity += Physics.gravity * Time.deltaTime * 3;
        }

        if (CanPlay)
        {
            _move_vec.x = _speed;
        }
        _move_vec += _gravity;
        _move_vec *= Time.deltaTime;

        CheckInput();

        _char_controller.Move(_move_vec);

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (_laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;
    }

    void CheckInput()
    {
        int sign = 0;
        if(!CanPlay)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            sign = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            sign = 1;
        }
        else
        {
            return;
        }

        _laneNumber += sign;
        _laneNumber = Mathf.Clamp(_laneNumber, 0, _lanesCount);       
    }

    IEnumerator Rolling()
    {
        _isRolling = true;

        _animator.SetBool("rolling", true);
        _char_controller.center = _ccCenterRoll;
        _char_controller.height = _ccHeightRoll;
        yield return new WaitForSeconds(1.5f);

        _isRolling = false;

        _animator.SetBool("rolling", false);

        _char_controller.center = _ccCenterNorm;
        _char_controller.height = _ccHeightNorm;
        
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!hit.gameObject.CompareTag("Trap"))
        {
            return;
        }

        if (LifesCount-1 > 0)
        {
            hit.collider.isTrigger = true;
            return;
        }

        StartCoroutine(Death());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        LifesCount--;
        
        Destroy(other.gameObject);
    }

    IEnumerator Death()
    {
        LifesCount = 0;
        CanPlay = false;
        _animator.SetBool("defeat", true);
        yield return new WaitForSeconds(2f);

       GM.ShowResult();
    }

    public void TurnOnRunAnimation()
    {
        _animator.SetBool("defeat", false);
    }
}
