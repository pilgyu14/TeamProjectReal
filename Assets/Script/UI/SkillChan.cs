using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillChan : MonoBehaviour
{
    private bool isChange = false; //ùŬ�� �ι�° Ŭ�� Ȯ�� 
    private  Sprite changedImage = null; // �̹� �����ϴ� �� �̹��� ��ü�� ��ü�� �̹��� 
    private int changed;
    public List<Image> skillImage = new List<Image>(); //��ųâ �� �̹��� 1 2 3

    public List<Sprite> skillSprite = new List<Sprite>(); //��ų ��������Ʈ 

    public List<GameObject> selected = new List<GameObject>(); //���Ӱ� ���� ���õ� ������Ʈ 
    GameObject clickObject;
    private void Start()
    {
       
        SpriteInit();
    }
    private void SpriteInit() //ó�� 3�� ��������Ʈ ��Ÿ���� 
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

    public void OnSetImage()// ���Ḧ ���� ���� ��ų�� Ŭ���� �� ������ �ֱ� 
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
        //OnChangeImage() �� �Լ� ����

    }
    public void OnChangeImage() //��ų 2�� ��ġ ���� 
    {
        
        if (!isChange)
        {
             GameObject clickObject = EventSystem.current.currentSelectedGameObject;
            Debug.Log("ù Ŭ��");
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
            Debug.Log("�ι�° Ŭ��");
           
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
