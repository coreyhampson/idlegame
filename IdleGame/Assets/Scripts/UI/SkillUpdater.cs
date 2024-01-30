using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpdater : MonoBehaviour
{
    [Header("Text")]
    public Text txtName;
    public Text txtExpAmount;
    public Text txtSpeed;
    public Text txtLocked;
    public Button btnCut;
    public Slider sliderExp;

    [Header("Groups")]
    public GameObject treeContent;
    public BaseSkillItemSO skillItemSO;

    private bool isActive;
    private float skillTimer = 0f;

    public bool isLocked => CheckIsLocked();

    private void Awake()
    {
        btnCut.onClick.AddListener(ToggleWoodcutting);
    }

    private bool CheckIsLocked()
    {
        switch (skillItemSO.GetType().Name)
        {
            case "WoodcuttingSO":
                return PlayerStats.instance.woodcuttingLevel < skillItemSO.UnlockLevel;

                // case "MiningSO":
        }

        return true;
    }

    public void UpdatePanel(BaseSkillItemSO skillItem)
    {
        skillItemSO = skillItem;

        txtName.text = skillItemSO.Name;
        txtExpAmount.text = skillItemSO.ExpAmount.ToString();
        txtSpeed.text = $"{skillItemSO.Speed:F2} Second{(skillItemSO.Speed > 1 ? "s" : string.Empty)}";
        UpdateLockStatus();
    }

    void ToggleWoodcutting()
    {
        isActive = !isActive;
        SetWoodcutting(isActive);
    }

    public void SetWoodcutting(bool setWoodcutting)
    {
        btnCut.GetComponentInChildren<Text>().text = setWoodcutting ? "Stop" : "Cut";

        if (setWoodcutting)
        {
            isActive = true;
            Skilling.instance.ActivateWoodcutting(this);
        }
        else
        {
            isActive = false;
            Skilling.instance.DeactivateWoodcutting(this);
            UpdateSlider();
        }
    }

    public void UpdateSlider()
    {
        if (skillTimer <= skillItemSO.Speed)
        {
            if (isActive)
            {
                skillTimer += Time.deltaTime;
                sliderExp.maxValue = skillItemSO.Speed;
                sliderExp.value = skillTimer;
            }
            else
            {
                skillTimer = 0f;
                sliderExp.value = 0f;
            }
        }
        else // Exp bar reached 100% (Exp gain achieved)
        {
            skillTimer = 0f;
            sliderExp.value = 0f;

            switch (skillItemSO.GetType().Name)
            {
                case "WoodcuttingSO":
                    GameManager.instance.AddItem(skillItemSO, 1);
                    Debug.LogWarning($"Item: {skillItemSO.name} does not have an Item Prefab assigned!");
                    PlayerStats.instance.AddWoodcuttingExperience(skillItemSO.ExpAmount);
                    PlayerStats.instance.SaveStats();
                    break;
            }

            Skilling.instance.refreshValues = true;
        }
    }

    public void UpdateLockStatus()
    {
        if (isLocked)
        {
            treeContent.SetActive(false);
            txtLocked.gameObject.SetActive(true);
            txtLocked.text = string.Format(Skilling.instance.unlockText, skillItemSO.UnlockLevel);
        }
        else
        {
            treeContent.SetActive(true);
            txtLocked.gameObject.SetActive(false);
        }
    }
}