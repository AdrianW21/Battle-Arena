using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

/*
 * This script done by Adrian Wisniewski is added to the main menu for buttons control.
 */

public class MainMenu : MonoBehaviour
{
    public playMenu playMenu;

    [SerializeField] private UIDocument m_UIDocument;
    public GameObject m_UIDoc;
    public GameObject play_UIDoc;
    public GameObject diff_UIDoc;
    public GameObject opt_UIDoc;
    public GameObject cred_UIDoc;
    public VisualElement root;
    public Button btnPlay;
    public Button btnDiff;
    public Button btnOpt;
    public Button btnCred;
    public Button btnExit;


    // Start is called before the first frame update
    void Start()
    {
        play_UIDoc.SetActive(false);
        diff_UIDoc.SetActive(false);
        opt_UIDoc.SetActive(false);
        cred_UIDoc.SetActive(false);
    }

    private void BtnPlay_clicked()
    {
        play_UIDoc.SetActive(true);
        m_UIDoc.SetActive(false);
        playMenu.menuLaunched = true;
    }

    private void BtnDiff_clicked()
    {
        diff_UIDoc.SetActive(true);
        m_UIDoc.SetActive(false);
    }

    private void BtnOpt_clicked()
    {
        opt_UIDoc.SetActive(true);
        m_UIDoc.SetActive(false);
    }

    private void BtnCred_clicked()
    {
        cred_UIDoc.SetActive(true);
        m_UIDoc.SetActive(false);
    }

    private void BtnExit_clicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        root = m_UIDocument.rootVisualElement;
        btnPlay = root.Q<Button>(name: "btnPlay");
        btnPlay.clicked += BtnPlay_clicked;
        btnDiff = root.Q<Button>(name: "btnDiff");
        btnDiff.clicked += BtnDiff_clicked;
        btnOpt = root.Q<Button>(name: "btnOpt");
        btnOpt.clicked += BtnOpt_clicked;
        btnCred = root.Q<Button>(name: "btnCred");
        btnCred.clicked += BtnCred_clicked;
        btnExit = root.Q<Button>(name: "btnExit");
        btnExit.clicked += BtnExit_clicked;
    }
}
