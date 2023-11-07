using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * This script done by Adrian Wisniewski is added to the difficulty menu for button control.
 * It was supposed to select the desired scene and number of characters based on the difficulty
 * chosen. Due to lack of time, this couldn't be done.
 */

public class DifficultyMenu : MonoBehaviour
{
    [SerializeField] private UIDocument diff_UIDocument;
    public GameObject m_UIDoc;
    public GameObject diff_UIDoc;
    public VisualElement root;
    public Button btnBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        root = diff_UIDocument.rootVisualElement;
        btnBack = root.Q<Button>(name: "btnBack");
        btnBack.clicked += BtnBack_clicked;
    }

    private void BtnBack_clicked()
    {
        diff_UIDoc.SetActive(false);
        m_UIDoc.SetActive(true);
    }
}
