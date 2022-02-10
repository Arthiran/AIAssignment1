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
        PlayerPrefs.SetString("FileLocation", Application.streamingAssetsPath + "\\probabilities.txt");
        PlayerPrefs.SetInt("DoorAmount", 20);
    }

    public void UpdateSliderText()
    {
        SliderValueText.text = Mathf.RoundToInt(DoorValueSlider.value).ToString();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("DoorAmount", Mathf.RoundToInt(DoorValueSlider.value));
        string NewText = InputText.text.Replace(@"\", "/");
        PlayerPrefs.SetString("FileLocation", NewText);
        SceneManager.LoadScene("GameScene");
    }
}
