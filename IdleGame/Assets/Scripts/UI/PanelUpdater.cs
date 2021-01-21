using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpdater : MonoBehaviour
{
    [Header("Text")]
    public Text txtName;
    public Text txtExpAmount;
    public Text txtSpeed;
    public Text txtLocked;
    public Button btnCut;

    [Header("Groups")]
    public GameObject treeContent;

    private TreeSO treeSO;
    private const string lockedText = "Unlocks at Level {0}";
    private bool isCutting;

    private bool isLocked => PlayerStats.instance.woodcuttingLevel < treeSO.UnlockLevel;
    
    public void UpdatePanel(TreeSO tree)
    {
        treeSO = tree;

        txtName.text = treeSO.name;
        txtExpAmount.text = treeSO.ExpAmount.ToString();
        txtSpeed.text = treeSO.Speed.ToString("F2");
        btnCut.onClick.AddListener(ToggleWoodcutting);

        UpdateLockStatus();
    }

    void ToggleWoodcutting()
    {
        isCutting = !isCutting;

        btnCut.GetComponentInChildren<Text>().text = isCutting ? "Stop" : "Cut";
        Skilling.instance.ActivateSkill(0);
    }

    public void UpdateLockStatus()
    {
        if (isLocked)
        {
            treeContent.SetActive(false);
            txtLocked.gameObject.SetActive(true);
            txtLocked.text = string.Format(lockedText, treeSO.UnlockLevel);
        }
        else
        {
            treeContent.SetActive(true);
            txtLocked.gameObject.SetActive(false);
        }
    }
}