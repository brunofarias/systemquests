using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BtnLevel : MonoBehaviour
{
  [Header("Config Level")]
  public Level level;
  public TextMeshProUGUI numberText;

  [Header("Config buttons")]
  public Image btnImage;
  public List<Sprite> btnSpriteImage;

  [Header("Config Quests")]
  public List<Image> quests;
  public Sprite questImage;

  private int idLevel;
  private Button button;
  private int completedQuests;
  private int completedAllQuests; 
  

  void Awake() {
    button = GetComponent<Button>();        
  }

  // Start is called before the first frame update
  void Start()
  {      
    idLevel = level.idLevel;
    completedQuests = PlayerPrefs.GetInt("completedQuests" + idLevel);
    completedAllQuests = PlayerPrefs.GetInt("completedAllQuests" + idLevel);
    
    loadLevel();
  }

  void loadLevel()
  { 
    numberText.SetText(idLevel.ToString());
    btnImage.sprite = btnSpriteImage[1];

    foreach (var quest in quests)
    {
      quest.gameObject.SetActive(false);
    }

    if (button.interactable)
    {      
      btnImage.sprite = btnSpriteImage[0];

      for (int i = 0; i < level.totalQuests; i++)
      {
        quests[i].gameObject.SetActive(true);
      }

      for (int j = 0; j < completedQuests; j++)
      {
        quests[j].sprite = questImage;
      }
    }      

    if (completedAllQuests == 1)
    {
      btnImage.sprite = btnSpriteImage[2];
    }

  }
}