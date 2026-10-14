using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
  public float moveSpeed = 5.0f;

  public Animator animator;

  public AudioSource aus;

  public int character;

  // Start is called before the first frame update
  void Start()
  {
  }

  bool isActivated = false;
  int lastSelected = 0;
  // Update is called once per frame
  void Update()
  {
    if(Input.anyKeyDown)
    {
      isActivated = true;
    }
    if(!isActivated) return;
    if(lastSelected != GameManager.gameManager.selected)
    {
      Debug.Log("scene changed");
      this.GetComponent<Rigidbody>().velocity = Vector3.zero;
      if(character != 2) {
        isActivated = false;
      }
      if (lastSelected == 2)
      {
        isActivated = true;
      }

      lastSelected = GameManager.gameManager.selected;
    }
    if (GameManager.gameManager.selected != character) return;
    bool is_floor = false;
    Vector3[] ray_poss = {
      new Vector3(0f, 0f, 0f),
      new Vector3(1f, 0f, 1f),
      new Vector3(1f, 0f, -1f),
      new Vector3(-1f, 0f, 1f),
      new Vector3(-1f, 0f, -1f),
    };
    for (int i = 0; i < 5; i++)
    {
      Ray ray = new Ray(transform.position + ray_poss[i], Vector3.down);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.distance <= 1.2)
        {
          is_floor = true;
          break;
        }
      }
    }

    Vector3 move = Vector3.zero;




    // 上矢印キー
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      move += Vector3.forward; // Z+方向
    }
    // 下矢印キー
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
      move += Vector3.back; // Z-方向
    }
    // 左矢印キー
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      move += Vector3.left; // X-方向
    }
    // 右矢印キー
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      move += Vector3.right; // X+方向
    }
    // 正規化して速度を一定に
    if (move != Vector3.zero)
    {
      aus.volume = 0.1f;
      move.Normalize();
      if(character == 0) {
        transform.rotation = Quaternion.LookRotation(-move, Vector3.up);
      }
      // transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
      float rate = is_floor ? 1.0f : 0.1f;
      this.GetComponent<Rigidbody>().AddForce(rate * move * moveSpeed * Time.deltaTime, ForceMode.Force);
      if(this.name == "Character2")
      {
        Debug.Log(rate * move * moveSpeed * Time.deltaTime);
      }
    } else
    {
      aus.volume = 0.02f;
    }
  }
}
