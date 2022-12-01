using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    public GameObject gOver;

    public GameObject HomeUI, inGameUI;
    public GameObject allBtn;

    private bool btns;

    public GameObject nextLevelUI;

    [Header("PreGame")]
    public Button soundBTN;
    public Sprite soundOnSpr, soundOffSpr;

    [Header("InGame")]
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;

    private Material playerMaterial;
    private Player player;

    void Awake()
    {
        playerMaterial = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<Player>();

        levelSlider.transform.parent.GetComponent<Image>().color = playerMaterial.color + Color.gray;
        levelSlider.color = playerMaterial.color;
        currentLevelImg.color = playerMaterial.color;
        nextLevelImg.color = playerMaterial.color;

        soundBTN.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
    }

    void Update()
    {
        if (player.playerState == Player.PlayerState.Prepare)
        {
            if (SoundManager.instance.sound && soundBTN.GetComponent<Image>().sprite != soundOnSpr)
                soundBTN.GetComponent<Image>().sprite = soundOnSpr;

            else if (!SoundManager.instance.sound && soundBTN.GetComponent<Image>().sprite != soundOffSpr)
                soundBTN.GetComponent<Image>().sprite = soundOffSpr;
        }
        if (Input.GetMouseButtonDown(0) && !IgnoreUI() && player.playerState == Player.PlayerState.Prepare) 
        {
            player.playerState = Player.PlayerState.Playing;
            HomeUI.SetActive(false);
            inGameUI.SetActive(true);
        }
        if (player.playerState == Player.PlayerState.Died)
        {
            gOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultList.Count > 0;
    }
    public void LevelSliderFill(float FillAmount)
    {
        levelSlider.fillAmount = FillAmount;
    }
    public void Settings()
    {
        btns = !btns;
        allBtn.SetActive(btns);
    }
    public void NextLevelUI()
    {
        nextLevelUI.SetActive(true);
    }
}
