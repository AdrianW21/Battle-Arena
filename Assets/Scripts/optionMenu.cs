using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * this script done by Adrian Wisniewski is added to the option menu.
 * It later can be used to manage settings of the game.
 */

public class optionMenu : MonoBehaviour
{
    [SerializeField] private UIDocument opt_UIDocument;
    public GameObject m_UIDoc;
    public GameObject opt_UIDoc;
    public VisualElement root;
    public Button btnBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        root = opt_UIDocument.rootVisualElement;
        btnBack = root.Q<Button>(name: "btnBack");
        btnBack.clicked += BtnBack_clicked;
    }

    private void BtnBack_clicked()
    {
        opt_UIDoc.SetActive(false);
        m_UIDoc.SetActive(true);
    }
}
