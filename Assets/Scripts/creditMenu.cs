using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

/*
 * This script done by Adrian Wisniewski is added to the credits menu for button control.
 */

public class creditMenu : MonoBehaviour
{
    [SerializeField] private UIDocument cred_UIDocument;
    public GameObject m_UIDoc;
    public GameObject cred_UIDoc;
    public VisualElement root;
    public Button btnBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void BtnBack_clicked()
    {
        cred_UIDoc.SetActive(false);
        m_UIDoc.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        root = cred_UIDocument.rootVisualElement;
        btnBack = root.Q<Button>(name: "btnBack");
        btnBack.clicked += BtnBack_clicked;
    }
}
