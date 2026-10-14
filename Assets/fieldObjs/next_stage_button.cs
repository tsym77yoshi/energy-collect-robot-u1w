using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next_stage_button : MonoBehaviour
{

  bool hasActivated = false;
  public GameObject ps;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!hasActivated && GameManager.gameManager.getIsQualifiedToNextStage())
    {
      ps.SetActive(true);
    }
  }
  void OnTriggerEnter(Collider other)
  {
    if (other.name == "Character")
    {
      if (GameManager.gameManager.getIsQualifiedToNextStage())
      {
        GameManager.gameManager.nextStage();
        //Destroy(gameObject.transform.parent.gameObject);
      }
    }
  }
}
