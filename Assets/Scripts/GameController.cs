using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum gameState { GAMEPLAY, END };

[RequireComponent(typeof(PreLevel))]
public class GameController : MonoBehaviour
{
  public gameState currentState;
  public PlayerController player;
  public Level level;

  [Header("Config Quest")]
  public List<GameObject> quests;
  public List<Image> imageQuests;
  public List<TextMeshProUGUI> textQuests;

  private PreLevel preLevel;
  private int idLevel = 0;
  private int currentLevel = 0;
  private int completedQuests = 0;

  void Awake()
  {
    preLevel = GetComponent<PreLevel>();
  }

  void Start()
  {
    idLevel = level.idLevel;
    completedQuests = PlayerPrefs.GetInt("completedQuests" + idLevel);
    print(completedQuests);

    foreach (var quest in quests)
    {
      quest.gameObject.SetActive(false);
    }

    for (int i = 0; i < level.totalQuests; i++)
    {
      quests[i].gameObject.SetActive(true);
    }

    remainQuests();
  }

  public void exitLevel()
  {
    currentState = gameState.END;
    levelUpdate();
    questsUpdate();
    preLevel.openModal(level);
  }

  void levelUpdate()
  {
    currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

    if (SceneManager.GetActiveScene().buildIndex != 4)
    {
      if (currentLevel >= PlayerPrefs.GetInt("levelUnlock"))
      {
        PlayerPrefs.SetInt("levelUnlock", currentLevel);
      }
    }
  }

  void questsUpdate()
  {
    for (int i = 0; i < level.quests.Count; i++)
    {
      switch (level.quests[i].nameQuest)
      {
        case "Diamantes":
          if (player.itemCount == level.quests[i].valueQuest && PlayerPrefs.GetInt("completedQuestsDiamantes" + idLevel) == 0)
          {
            completedQuests++;
            PlayerPrefs.SetInt("completedQuestsDiamantes" + idLevel, 1);
          }
          break;

        case "Pulos":
          if (player.jumpCount == level.quests[i].valueQuest && PlayerPrefs.GetInt("completedQuestsPulos" + idLevel) == 0)
          {
            completedQuests++;
            PlayerPrefs.SetInt("completedQuestsPulos" + idLevel, 1);
          }
          break;

        case "Inimigos":
          if (player.enemyCount == level.quests[i].valueQuest && PlayerPrefs.GetInt("completedQuestsInimigos" + idLevel) == 0)
          {
            completedQuests++;
            PlayerPrefs.SetInt("completedQuestsInimigos" + idLevel, 1);
          }
          break;
      }

      PlayerPrefs.SetInt("completedQuests" + idLevel, completedQuests);

    }

    if (level.totalQuests == completedQuests)
    {
      PlayerPrefs.SetInt("completedAllQuests" + idLevel, 1);
    }
  }

  public void remainQuests()
  {
    for (int i = 0; i < level.quests.Count; i++)
    {
      switch (level.quests[i].nameQuest)
      {
        case "Diamantes":
          imageQuests[i].sprite = level.quests[i].imageQuests;
          textQuests[i].SetText(player.itemCount + "/" + level.quests[i].valueQuest);
          break;

        case "Pulos":
          imageQuests[i].sprite = level.quests[i].imageQuests;
          textQuests[i].SetText(player.jumpCount + "/" + level.quests[i].valueQuest);
          break;

        case "Inimigos":
          imageQuests[i].sprite = level.quests[i].imageQuests;
          textQuests[i].SetText(player.enemyCount + "/" + level.quests[i].valueQuest);
          break;
      }
    }
  }

  public void reloadLevel()
  {
    currentState = gameState.GAMEPLAY;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void nextLevel()
  {
    if (SceneManager.GetActiveScene().buildIndex != 4)
    {
      currentState = gameState.GAMEPLAY;
      SceneManager.LoadScene(0);
      PlayerPrefs.SetInt("nextBtnLevel", 1);
    }
  }

  public void backHome()
  {
    currentState = gameState.GAMEPLAY;
    SceneManager.LoadScene(0);
  }
}
