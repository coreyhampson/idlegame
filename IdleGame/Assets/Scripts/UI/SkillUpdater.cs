using Assets.Scripts.ScriptableObjects;
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

    private const string lockedText = "Unlocks at Level {0}!";
    private bool isActive;
    private float skillTimer = 0f;

    public bool isLocked => CheckIsLocked();

    private bool CheckIsLocked()
    {
        switch (skillItemSO.GetType().Name)
        {
            case "WoodcuttingSO":
                btnCut.onClick.AddListener(ToggleWoodcutting);
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
        else
        {
            skillTimer = 0f;
            sliderExp.value = 0f;

            switch (skillItemSO.GetType().Name)
            {
                case "WoodcuttingSO":
                    GameManager.instance.AddItem("Evergreen Log", 1);
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
            txtLocked.text = string.Format(lockedText, skillItemSO.UnlockLevel);
        }
        else
        {
            treeContent.SetActive(true);
            txtLocked.gameObject.SetActive(false);
        }
    }
}