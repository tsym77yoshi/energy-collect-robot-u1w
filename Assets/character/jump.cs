using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
  public Animator animator;

  public int character;

  public float jump_force;
  public float down_force;
  Rigidbody rigidbody;
  Ray ray;

  // Start is called before the first frame update
  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  float jump_motion_time = -1;

  // Update is called once per frame
  void Update()
  {
    bool is_floor = false;
    Vector3[] ray_poss = {
      new Vector3(0f, 0f, 0f),
      new Vector3(1f, 0f, 1f),
      new Vector3(1f, 0f, -1f),
      new Vector3(-1f, 0f, 1f),
      new Vector3(-1f, 0f, -1f),
    };
    for (int i = 0; i < 1; i++)
    {
      Ray ray = new Ray(transform.position + ray_poss[i], Vector3.down);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        // Debug.Log(i+": "+ hit.distance);
        if (hit.distance <= 1.2f)
        {
          is_floor = true;
          break;
        }
      }
    }
    if (GameManager.gameManager.selected == 2 && is_floor)
    {
      GameManager.gameManager.selected = 0;
    }
    if (0 <=jump_motion_time && jump_motion_time < 0.2)
    {
      animator.SetBool("IsFloor", false);
    } else {
      animator.SetBool("IsFloor", is_floor);
      if (is_floor)
      {
        jump_motion_time = -1;
      }
    }
    if (GameManager.gameManager.selected == 1 || GameManager.gameManager.selected == 2)
    {
      jump_motion_time = -1;
    }

    if (jump_motion_time >= 0)
    {
      jump_motion_time += Time.deltaTime;
      if (jump_motion_time < 0.1)
      {

      }
      else if (jump_motion_time < 0.2)
      {
        rigidbody.AddForce(Vector3.up * jump_force * Time.deltaTime, ForceMode.Force);
      }
      else if (jump_motion_time < 0.5)
      {
        rigidbody.AddForce(Vector3.up * 9.8f * 0.9f * Time.deltaTime, ForceMode.Force);
      }
      else if (jump_motion_time < 1.0f)
      {
        rigidbody.AddForce(Vector3.down * down_force * Time.deltaTime, ForceMode.Force);
      }
      return;
    }
    if (Input.GetKeyDown(KeyCode.Space) && is_floor && GameManager.gameManager.selected == character)
    {
      Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
      animator.SetTrigger("Jump");
      jump_motion_time = 0;

      // if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") == false){
      // }
    }
  }
}