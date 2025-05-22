using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<AvatarSelect> avatarSelect;
    public AvatarSelect selectedAvatar;
    public Image avatatImage;
    public int yourAvatarId;
    public TMP_Text pageText;
    public Button prevBtn;
    public Button nextBtn;
    public Transform armachuture;
    public Animator charAvatar;
    
    void Start()
    {
        AvatarUIData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        yourAvatarId++;
        AvatarUIData();
    }

    public void Prev()
    {
        yourAvatarId--;
        AvatarUIData();
    }

    public void AvatarUIData()
    {
        bool next = false;
        bool prev = false;
        for (int i = 0; i < avatarSelect.Count; i++)
        {
            if (yourAvatarId == avatarSelect[i].id)
            {
                avatatImage.sprite = avatarSelect[i].avatarSprite;
                pageText.text = yourAvatarId.ToString() + " / " + avatarSelect.Count;
                break;
            }
        }

        for(int i = 0;i < avatarSelect.Count;i++)
        {
            if(yourAvatarId + 1 == avatarSelect[i].id)
            {
                next = true;
                break;
            }
        }

        for (int i = 0; i < avatarSelect.Count; i++)
        {
            if (yourAvatarId - 1 == avatarSelect[i].id)
            {
                prev = true;
                break;
            }
        }

        if(next)
            nextBtn.interactable = true;
        else
            nextBtn.interactable = false;

        if (prev)
            prevBtn.interactable = true;
        else
            prevBtn.interactable = false;
    }

    public void SelectAvatar()
    {
        selectedAvatar = avatarSelect[yourAvatarId - 1];

        charAvatar.avatar = selectedAvatar.avatar;

        GameObject mesh = Instantiate(selectedAvatar.mesh.gameObject, armachuture);
        mesh.transform.localPosition = Vector3.zero;
    }
        
}

[Serializable]
public class AvatarSelect
{
    public int id;
    public Sprite avatarSprite;
    public Transform mesh;
    public Avatar avatar;
}

