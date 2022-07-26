using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    //BULLET
    [Header("BULLET")]
    public Slider bulletAmountSlider;
    public TextMeshProUGUI bulletAmountText, maxBulletAmountText, waveText;
    public Image weaponUiImage;
    public SpriteRenderer inGameTexture;

    //HEALTH
    [Header("HealthBars")]
    public Slider healthBar;
    public Slider staminaBar, zombieHealthBar;
    //MINE
    [Header("MINE")]
    public TextMeshProUGUI mineAmountText;

    //COLLECTABLES
    [Header("Collectables")]
    public Transform collectables;

    public GameObject[] UIElements;

    public void BulletAmount(int value0, int value1, int value2)//current ammo, mag capasite, total ammo
    {
        bulletAmountSlider.maxValue = value1;
        bulletAmountSlider.value = value0;

        bulletAmountText.text = value0.ToString();
        maxBulletAmountText.text = value2.ToString();
    }
    public void MineAmount(int value)
    {
        mineAmountText.text = value.ToString();
    }
    public void NextWave(int waveCount)
    {
        if (waveCount % 5 != 0) waveText.text = "wave " + waveCount.ToString();
        else waveText.text = "Boss fight ";

        Animator animator = waveText.GetComponent<Animator>();
        animator.SetTrigger("waveStarts");

    }
    public void UpdateUI(WeaponDatabase weaponDatabase)
    {
        //UI güncelle
        int weaponId = PlayerPrefs.GetInt("selectedWeapon", 0);

        Weapon weapon = weaponDatabase.GetWeapon(weaponId);

        inGameTexture.sprite = weapon.inGameSprite;//in game sprite

        weaponUiImage.sprite = weapon.imageUI;//ui image

        bulletAmountSlider.maxValue = weapon.magCapasite; //bullet sliders
        bulletAmountSlider.value = weapon.currentAmmo;

        bulletAmountText.text = weapon.currentAmmo.ToString();//bullet texts
        maxBulletAmountText.text = weapon.totalAmmo.ToString();

        mineAmountText.text = weapon.currentAmmo.ToString();//mine text

        for (int i = 0; i <= UIElements.Length - 1; i++) //control every object
        {
            UIElements[i].SetActive(false); //don't show
        }
        UIElements[weaponId].SetActive(true); //show selected weapon on UI
    }
    public void SetMaxHealth(int value, Slider slider)
    {
        slider.maxValue = value;
        slider.value = slider.maxValue;

        if (slider == zombieHealthBar) zombieHealthBar.gameObject.SetActive(true);
    }
    public void SetHealth(int value, Slider slider)
    {
        slider.value = value;
    }
    public void SetMaxStamina(int value)
    {
        staminaBar.maxValue = value;
        staminaBar.value = staminaBar.maxValue;
    }
    public void SetStamina(int value)
    {
        staminaBar.value = value;
    }
    public void WriteCollectables(GameObject prefab, string text)
    {
        GameObject writed = Instantiate(prefab,collectables);
        writed.GetComponent<TextMeshProUGUI>().text = text;
        Destroy(writed, 2f);
    }

}
