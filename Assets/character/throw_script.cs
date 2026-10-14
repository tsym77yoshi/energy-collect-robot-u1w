using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class throw_script : MonoBehaviour
{
  public PositionConstraint positionConstraint;

  public GameObject character1;
  public GameObject character2;
  Rigidbody c1rb;
  Rigidbody c2rb;

  public Vector3 impulse;
    // Start is called before the first frame update
    void Start()
    {
      c1rb = character1.GetComponent<Rigidbody>();
      c2rb = character2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && GameManager.gameManager.selected == 1)
      {
        GameManager.gameManager.selected = 2;
        positionConstraint.enabled = false;
        c2rb.velocity = Vector3.zero;
        c1rb.AddForce(impulse, ForceMode.Impulse);
      }
    }
}
