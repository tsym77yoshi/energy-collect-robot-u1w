using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

using UnityEngine.SceneManagement;

public enum Selected
{
  ROBO1,
  ROBO_THROW
}
public class GameManager : MonoBehaviour
{
  public static GameManager gameManager;
  public int selected;// 操作するキャラクター
  public int got_star_count;
  public Vector3 start_pos;

  int[] requireStarCounts = {5, 15, 30, 50, 100};
  public Transform[] start_poss;
  public int stage = 0;

  public GameObject[] nextStageButtons;
  public GameObject[] damageLaserParents;

  public TextMeshProUGUI[] scores;
  public TextMeshProUGUI stageTMPRo;

  public AudioSource scoreAus;
  public AudioSource stageAus;
  public AudioSource qualifiedAus;

  public PlayableDirector clearPD;
  public Image[] stars;
  public Sprite onStar;
  public Sprite notStar;

  // Start is called before the first frame update
  void Start()
  {
    selected = 0;
    stage = 0;
    got_star_count = 0;
    // インスタンスがまだ作られていなければ自分を代入
    if (gameManager == null)
    {
      gameManager = this;
    }
    init();
  }

  public float totalTime = 0;
  bool isTime = true;
  // Update is called once per frame
  void Update()
  {
    totalTime += isTime ? Time.deltaTime : 0;

  }


  public void getStar(int star_size)
  {
    bool isLastQualified = getIsQualifiedToNextStage();
    got_star_count += star_size;
    setScore();
    if(!isLastQualified && getIsQualifiedToNextStage())
    {
      if(stage >= 4)
      {
        // クリア
        Debug.Log("clear");
        isTime = false;
        clearPD.Play();
        Invoke(nameof(DelayMethod), 3.5f);
        int scoreLevel = 0;
        if(totalTime < 210)
        {
          scoreLevel = 5;
        } else if(totalTime < 240)
        {
          scoreLevel = 4;
        } else if(totalTime < 270)
        {
          scoreLevel = 3;
        } else if(totalTime < 300)
        {
          scoreLevel = 2;
        } else if(totalTime < 330)
        {
          scoreLevel = 1;
        }
        for(int i = 0;i < stars.Length;i++)
        {
          stars[i].sprite = scoreLevel > i ? onStar : notStar;
        }
      }
      qualifiedAus.Play();

    } else {
      scoreAus.Play();
    }
  }
  void DelayMethod()
  {
    Debug.Log("to result");
  }
  void setScore()
  {
    for(int i = 0;i < scores.Length;i++)
    {
      string text = "ENERGY " + got_star_count + "/" + requireStarCounts[stage];
      scores[i].SetText(text);
      Debug.Log(text);

      scores[i].color = getIsQualifiedToNextStage() ? Color.green : Color.red;
    }
  }
  public bool getIsQualifiedToNextStage()
  {
    return requireStarCounts[stage] <= got_star_count;
  }
  public void nextStage()
  {
    damageLaserParents[stage].SetActive(false);
    stage++;
    for(int i = 0;i < nextStageButtons.Length;i++)
    {
      nextStageButtons[i].SetActive(i == stage);
    }
    start_pos = start_poss[stage].position;
    stageAus.Play();
    setScore();
    stageTMPRo.SetText("STAGE " + (stage + 1) + "/5");
  }
  void init()
  {
    Debug.Log("init");
    got_star_count = 0;
    stage = 0;
    for(int i = 0; i < nextStageButtons.Length;i++)
    {
      nextStageButtons[i].SetActive(i == stage);
    }
    start_pos = start_poss[stage].position;
    Debug.Log(start_poss[stage].position);
    setScore();
  }
}
