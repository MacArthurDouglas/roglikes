using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Image healthBarForeground; // 血条前景
    private int currentEnergy; // 死亡数量
    private const int maxEnergy = 30;   // 最大死亡数量
    public static int energy;

    void Start()
    {
        currentEnergy = 0;
        energy = currentEnergy;
        UpdateEnergyBar();
    }

    // 调用此方法以增加死亡数量
    public void OnMonsterDeath()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy++;
            energy = currentEnergy;
            UpdateEnergyBar();
        }
        if (currentEnergy >= maxEnergy)
        {
            PlayerControl.SwitchSurface();
            currentEnergy = 0;
            UpdateEnergyBar() ;
        }
    }
    public static int GetEnergy() { 
        return energy;
    }
    // 更新血条的显示
    private void UpdateEnergyBar()
    {
        float fillAmount = (float)currentEnergy / maxEnergy;
        healthBarForeground.fillAmount = fillAmount;
    }
}