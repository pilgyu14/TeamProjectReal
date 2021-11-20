using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillChan : MonoBehaviour
{
    private bool isChange = false; //첫클릭 두번째 클릭 확인 
    private  Sprite changedImage = null; // 이미 존재하는 두 이미지 교체시 교체될 이미지 
    private int changed;
    public List<Image> skillImage = new List<Image>(); //스킬창 내 이미지 1 2 3

    public List<Sprite> skillSprite = new List<Sprite>(); //스킬 스프라이트 

    public List<GameObject> selected = new List<GameObject>(); //새롭게 얻은 선택될 오브젝트 
    GameObject clickObject;
    private void Start()
    {
       
        SpriteInit();
    }
    private void SpriteInit() //처음 3개 스프라이트 나타내기 
    {
        for(int i =0; i<selected.Count;i++)
        {
            selected[i].GetComponent<Image>().sprite = skillSprite[i];
        }

        for (int i = 0; i < skillImage.Count; i++)
        {
            skillImage[i] = skillImage[i].GetComponent<Image>();
        }

    }
    

    //void Assign()
    //{
    //    for(int i = 0; i<selected.Count;i++)
    //    {
    //        selected[i].
    //    }
    //}

    public void OnSetImage()// 동료를 통해 얻은 스킬을 클릭시 빈 공간에 넣기 
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        for(int i =0; i<skillImage.Count; i++)
        {
            if (skillImage[i].sprite == null)
            {
                SetColor(skillImage[i], 1);
                skillImage[i].sprite = clickObject.GetComponent<Image>().sprite;
                return;
            }

        }
        //if (skillImage[0].sprite == null)
        //{
        //    SetColor(skillImage[0], 1);
        //    skillImage[0].sprite = clickObject.GetComponent<Image>().sprite;
        //}

        //else if (skillImage[1].sprite == null)
        //{
        //    SetColor(skillImage[0], 1);
        //    skillImage[1].sprite = clickObject.GetComponent<Image>().sprite;
        //} 

        //else if (skillImage[2].sprite == null)
        //{
        //    SetColor(skillImage[0], 1);
        //    skillImage[2].sprite = clickObject.GetComponent<Image>().sprite;
        //}
        clickObject.SetActive(false);
        //else
        //OnChangeImage() 이 함수 실행

    }
    public void OnChangeImage() //스킬 2개 위치 변경 
    {
        
        if (!isChange)
        {
             GameObject clickObject = EventSystem.current.currentSelectedGameObject;
            Debug.Log("첫 클릭");
            if (clickObject.CompareTag("skill1"))
                changed = 0;

            else if (clickObject.CompareTag("skill2"))
                changed = 1;
            else if (clickObject.CompareTag("skill3"))
                changed = 2;

            //changeImage = gameObject.GetComponent<Image>().sprite;
            isChange = true;
        }
        else
        {
            GameObject clickObjectSec = EventSystem.current.currentSelectedGameObject;
            Debug.Log("두번째 클릭");
           
            Sprite tempSprite = SecondClick(changed).sprite;
            SecondClick(changed).sprite = clickObjectSec.GetComponent<Image>().sprite;
            clickObjectSec.GetComponent<Image>().sprite = tempSprite;
            changed = 0; 
            isChange = false;
        }
    }

    public Image SecondClick(int changedImage)
    {
        for(int i = 0; i<skillImage.Count;i++)
        {
            if (changedImage == i)
                return skillImage[i]; 
        }
        return skillImage[0]; 
    }
     
    public void SetColor(Image image,float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color; 
    }
}
