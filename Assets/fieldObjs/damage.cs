using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class damage : MonoBehaviour
{
  public GameObject character1;
  public GameObject character2;
  public PositionConstraint positionConstraint;
  AudioSource damageSound;
  // Start is called before the first frame update
  void Start()
  {
    damageSound = GameObject.Find("DamageSound").GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnTriggerEnter(Collider other)
  {
    Debug.Log(GameManager.gameManager.selected);
    if (other.name == "Character" && GameManager.gameManager.selected != 2)
    {
      damageSound.Play();
      GameManager.gameManager.selected = 0;
      positionConstraint.enabled = false;
      character1.GetComponent<Rigidbody>().velocity = Vector3.zero;
      character1.transform.position = GameManager.gameManager.start_pos;
      character2.GetComponent<Rigidbody>().velocity = Vector3.zero;
      character2.transform.position = GameManager.gameManager.start_pos + new Vector3(0, 0, 10);
    }
  }
}
