using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Image healthBarForeground; // 血条前景
    private int monsterDeathCount; // 死亡数量
    private const int maxDeaths = 30;   // 最大死亡数量
    public static int energy;

    void Start()
    {
        monsterDeathCount = 0;
        energy = monsterDeathCount;
        UpdateHealthBar();
    }

    // 调用此方法以增加死亡数量
    public void OnMonsterDeath()
    {
        if (monsterDeathCount < maxDeaths)
        {
            monsterDeathCount++;
            energy = monsterDeathCount;
            UpdateHealthBar();
        }
    }
    public static int GetEnergy() { 
        return energy;
    }
    // 更新血条的显示
    private void UpdateHealthBar()
    {
        float fillAmount = (float)monsterDeathCount / maxDeaths;
        healthBarForeground.fillAmount = fillAmount;
    }
}