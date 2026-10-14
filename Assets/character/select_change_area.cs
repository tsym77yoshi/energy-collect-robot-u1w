using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class select_change_area : MonoBehaviour
{
  public GameObject character1;
  public GameObject character2;
  public PositionConstraint positionConstraint;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
  }
  void OnTriggerEnter(Collider other)
  {
    Debug.Log("coli");
    if (other.name == "Character")
    {
      Debug.Log("味方と衝突した！");
      GameManager.gameManager.selected = 1;
      positionConstraint.enabled = true;

      other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
      other.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
      character2.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
  }
}
