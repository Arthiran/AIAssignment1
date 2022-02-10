// Arthiran Sivarajah - 100660300
// 2022/02/09
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Slider DoorValueSlider;
    public Text SliderValueText;
    public InputField InputText;

    private void Start()
    {
        // Sets default values
        PlayerPrefs.SetString("FileLocation", Application.streamingAssetsPath + "\\probabilities.txt");
        PlayerPrefs.SetInt("DoorAmount", 20);
    }

    public void UpdateSliderText()
    {
        // Updates slider value to make value obvious
        SliderValueText.text = Mathf.RoundToInt(DoorValueSlider.value).ToString();
    }

    public void StartGame()
    {
        // Switches to game scene and updates game values
        PlayerPrefs.SetInt("DoorAmount", Mathf.RoundToInt(DoorValueSlider.value));
        string NewText = InputText.text.Replace(@"\", "/");
        PlayerPrefs.SetString("FileLocation", NewText);
        SceneManager.LoadScene("GameScene");
    }
}
