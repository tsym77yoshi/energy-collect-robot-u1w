using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mainUI : MonoBehaviour
{
  public static mainUI mainUInstance;

  public TextMeshProUGUI score;
  public TextMeshProUGUI time;
  public TextMeshProUGUI debug;

  int timeInt = 0;
  // Start is called before the first frame update
  void Start()
  {
  }
  // Update is called once per frame
  void Update()
  {
    // score.SetText("SCORE " + GameManager.gameManager.got_star_count);
    if (timeInt != (int)GameManager.gameManager.totalTime)
    {
      timeInt = (int)GameManager.gameManager.totalTime;
      string minStr = "" + (int)(timeInt / (int)60);
      string secStr = "" + (timeInt % 60);
      if (minStr.Length == 1)
      {
        minStr = "0" + minStr;
      }
      if (secStr.Length == 1)
      {
        secStr = "0" + secStr;
      }
      time.SetText("TIME " + minStr + ":" + secStr);
    }
    // debug.SetText("debug"+GameManager.gameManager.selected);
  }
  public void changeScore()
  {

  }
}
