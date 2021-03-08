using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
  public Button[] lvlButtons;
  private int levelUnlock = 0;
  private int nextBtnLevel = 0;

  void Start()
  {
    levelUnlock = PlayerPrefs.GetInt("levelUnlock", 1);
    nextBtnLevel = PlayerPrefs.GetInt("nextBtnLevel");

    print(levelUnlock);

    for (int i = 0; i < lvlButtons.Length; i++)
    {
      lvlButtons[i].interactable = false;
    }

    for (int i = 0; i < levelUnlock; i++)
    {
      lvlButtons[i].interactable = true;
    }

    if (nextBtnLevel == 1)
    {
      StartCoroutine(modalNextLevel());
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      PlayerPrefs.DeleteAll();
    }
  }

  IEnumerator modalNextLevel()
  {
    yield return new WaitForSeconds(1f);    
    lvlButtons[levelUnlock - 1].onClick.Invoke();  
    PlayerPrefs.SetInt("nextBtnLevel", 0);  
  }
}


