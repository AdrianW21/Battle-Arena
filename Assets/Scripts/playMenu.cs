using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * The script for the character selection menu was done by Adrian Wisniewski
 */


public class playMenu : MonoBehaviour
{
    public AttriPerso attriPerso;
    public GameManager gameManager;

    [SerializeField] private UIDocument char_UIDocument;

    public GameObject m_UIDoc;
    public GameObject char_UIDoc;

    public VisualElement root;

    public Button btnCh1;
    public Button btnCh2;
    public Button btnCh3;
    public Button btnCh4;
    public Button btnCh5;
    public Button btnCh6;
    public Button btnStart;
    public Button btnBack;

    public GameObject camMain;
    public GameObject camMenu;
    public GameObject game;

    public GameObject ch1;
    public GameObject ch2;
    public GameObject ch3;      
    public GameObject ch4;
    public GameObject ch5;
    public GameObject ch6;

    public GameObject Brute;
    public GameObject Archer;

    public bool menuLaunched = false;

    public List<GameObject> charactersPrefabs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuLaunched)
        {
            root = char_UIDocument.rootVisualElement;
            btnCh1 = root.Q<Button>(name: "btnCh1");
            btnCh1.clicked += BtnCh1_clicked;
            btnCh2 = root.Q<Button>(name: "btnCh2");
            btnCh2.clicked += BtnCh2_clicked;
            btnCh3 = root.Q<Button>(name: "btnCh3");
            btnCh3.clicked += BtnCh3_clicked;
            btnCh4 = root.Q<Button>(name: "btnCh4");
            btnCh4.clicked += BtnCh4_clicked;
            btnCh5 = root.Q<Button>(name: "btnCh5");
            btnCh5.clicked += BtnCh5_clicked;
            btnCh6 = root.Q<Button>(name: "btnCh6");
            btnCh6.clicked += BtnCh6_clicked;
            btnStart = root.Q<Button>(name: "btnStart");
            btnStart.clicked += BtnStart_clicked;
            btnBack = root.Q<Button>(name: "btnBack");
            btnBack.clicked += BtnBack_clicked;
            menuLaunched = false;
        }
    }

    private void BtnCh1_clicked()
    {
        // Switch between "Brute and "Archer"
        if (btnCh1.text == attriPerso.Brute.classe)
        {
            btnCh1.text = attriPerso.Archer.classe;
            ch1 = Archer;
        }
        else
        {
            btnCh1.text = attriPerso.Brute.classe;
            ch1 = Brute;
        }
    }
    private void BtnCh2_clicked()
    {
        if (btnCh2.text == attriPerso.Brute.classe)
        {
            btnCh2.text = attriPerso.Archer.classe;
            ch2 = Archer;
        }
        else
        {
            btnCh2.text = attriPerso.Brute.classe;
            ch2 = Brute;
        }
    }
    private void BtnCh3_clicked()
    {
        if (btnCh3.text == attriPerso.Brute.classe)
        {
            btnCh3.text = attriPerso.Archer.classe;
            ch3 = Archer;
        }
        else
        {
            btnCh3.text = attriPerso.Brute.classe;
            ch3 = Brute;
        }
    }
    private void BtnCh4_clicked()
    {
        if (btnCh4.text == attriPerso.Brute.classe)
        {
            btnCh4.text = attriPerso.Archer.classe;
            ch4 = Archer;
        }
        else
        {
            btnCh4.text = attriPerso.Brute.classe;
            ch4 = Brute;
        }
    }
    private void BtnCh5_clicked()
    {
        if (btnCh5.text == attriPerso.Brute.classe)
        {
            btnCh5.text = attriPerso.Archer.classe;
            ch5 = Archer;
        }
        else
        {
            btnCh5.text = attriPerso.Brute.classe;
            ch5 = Brute;
        }
    }
    private void BtnCh6_clicked()
    {
        if (btnCh6.text == attriPerso.Brute.classe)
        {
            btnCh6.text = attriPerso.Archer.classe;
            ch6 = Archer;
        }
        else
        {
            btnCh6.text = attriPerso.Brute.classe;
            ch6 = Brute;
        }
    }
    private void BtnStart_clicked()
    {
        camMenu.SetActive(false);
        camMain.SetActive(true);
        charactersPrefabs.Add(ch1);
        charactersPrefabs.Add(ch2);
        charactersPrefabs.Add(ch3);
        charactersPrefabs.Add(ch4);
        charactersPrefabs.Add(ch5);
        charactersPrefabs.Add(ch6);
        game.SetActive(true);
        gameManager.gameStarted = true;
        char_UIDoc.SetActive(false);
    }

    private void BtnBack_clicked()
    {
        menuLaunched = false;
        char_UIDoc.SetActive(false);
        m_UIDoc.SetActive(true);
    }
}
