using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SynergySeeBox : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler
{
    public Text m_tName;
    public Text m_tContent;
    public GameObject SeeBox;
    public int index;

    public void OnPointerDown(PointerEventData eventData)
    {
        CameraCs.m_bCameraDreg = false;
        SeeBox.SetActive(true);
        switch (index)
        {
            case 0:
                m_tName.text = "Connon " + TowerManager.m_Propertycount[5].ToString() + " / 3";
                m_tContent.text = "캐논의 10% / 20% / 30% 공격력 증가";
                break;
            case 1:
                m_tName.text = "School " + TowerManager.m_Propertycount[4].ToString() + " / 3";
                m_tContent.text = "병사의 10 / 20 / 30 체력 증가";
                break;
            case 2:
                m_tName.text = "Bow " + TowerManager.m_Propertycount[1].ToString() + " / 4\n" +
                    "CrossBow " + TowerManager.m_Propertycount[2].ToString() + " / 2";
                m_tContent.text = "활과 석궁의 다수 공격 부여";
                break;
            case 3:
                m_tName.text = "Bow " + TowerManager.m_Propertycount[1].ToString() + "/ 2\n" +
                   "CrossBow " + TowerManager.m_Propertycount[2].ToString() + " / 2\n" +
                   "Magician " + TowerManager.m_Propertycount[3].ToString() + " / 2";
                m_tContent.text = "활, 석궁, 마법사에게 매초 3 / 5 / 7 지속 데미지 부여";
                break;
            case 4:
                m_tName.text = "CrossBow " + TowerManager.m_Propertycount[2].ToString() + "/ 3";
                m_tContent.text = "석궁의 10% / 20% / 30% 공격 범위 증가";
                break;
            case 5:
                m_tName.text = "Magician " + TowerManager.m_Propertycount[3].ToString() + "/ 3";
                m_tContent.text = " 마법사의 30% / 40% / 50% 슬로우 부여";
                break;
            case 6:
                m_tName.text = "Bow " + TowerManager.m_Propertycount[1].ToString() + " / 3";
                m_tContent.text = " 활의  20% / 30% / 40% 공격 속도 증가";
                break;
            case 7:
                m_tName.text = "CrossBow " + TowerManager.m_Propertycount[2].ToString() + " /2\n" +
                   "Magician " + TowerManager.m_Propertycount[3].ToString() + " / 2\n" +
                   "Connon " + TowerManager.m_Propertycount[5].ToString() + " / 2";
                m_tContent.text = "석궁, 마법사, 캐논에게  1초 기절 부여";
                break;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CameraCs.m_bCameraDreg = true;
        SeeBox.SetActive(false);
    }
}
