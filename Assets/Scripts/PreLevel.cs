using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLevel : MonoBehaviour
{
  public enum state
  {
    LEVEL, GAMEPLAY
  }

  public state statePlay;
  public GameObject preLevel;
  public GameObject buttonsHome;
  public GameObject buttonsGame;
  public GameObject buttonClose;
  public TextMeshProUGUI numberLevel;
  public TextMeshProUGUI nameLevel;
  public TextMeshProUGUI restQuest;
  public TextMeshProUGUI allQuest;
  public List<GameObject> quests;
  public List<Image> imageQuests;
  public List<TextMeshProUGUI> textQuests;

  private int idLevel;
  private string nameQuest;
  private int tempQuest;
  private int completedQuests;
  private int completedAllQuests;


  void Start()
  {
    preLevel.SetActive(false);
  }

  public void openModal(Level level)
  {
    idLevel = level.idLevel;
    numberLevel.SetText("");
    nameLevel.SetText("");
    completedAllQuests = PlayerPrefs.GetInt("completedAllQuests" + idLevel);  

    foreach (var quest in quests)
    {
      quest.gameObject.SetActive(false);
    }

    if (statePlay == state.GAMEPLAY)
    {
      buttonsGame.SetActive(true);
      buttonsHome.SetActive(false);
      buttonClose.SetActive(false);
      restQuest.gameObject.SetActive(false);
      allQuest.gameObject.SetActive(false);

      for (int i = 0; i < level.totalQuests; i++)
      {
        nameQuest = level.quests[i].nameQuest;
        completedQuests = PlayerPrefs.GetInt("completedQuests" + nameQuest + idLevel); 

        if (completedQuests == 0)
        {
          quests[i].gameObject.SetActive(true);
          restQuest.gameObject.SetActive(true);
          imageQuests[i].sprite = level.quests[i].imageQuests;
          textQuests[i].text = level.quests[i].textQuests;
          textQuests[i].color = Color.red;
        }
      }

      if (completedAllQuests == 1)
      {
        allQuest.gameObject.SetActive(true);
        allQuest.color = Color.yellow;
      }
    }
    else
    {
      buttonsHome.SetActive(true);
      buttonClose.SetActive(true);
      buttonsGame.SetActive(false);
      
      for (int i = 0; i < level.totalQuests; i++)
      {
        quests[i].gameObject.SetActive(true);
        imageQuests[i].sprite = level.quests[i].imageQuests;
        textQuests[i].text = level.quests[i].textQuests;
        nameQuest = level.quests[i].nameQuest;
        completedQuests = PlayerPrefs.GetInt("completedQuests" + nameQuest + idLevel); 

        if (completedQuests == 1) 
        {
          textQuests[i].color = Color.yellow;
        }
        else 
        {
          textQuests[i].color = Color.white;
        }
      }
    }

    numberLevel.SetText(level.numberLevel.ToString());
    nameLevel.SetText(level.nameLevel.ToString());

    preLevel.SetActive(true);
  }
  
  public void loadScene()
  {
    SceneManager.LoadScene("Level" + idLevel);
  }
}