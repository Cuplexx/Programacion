using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Singleton;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    [SerializeField] private Dialogue currenDialogue;
    [SerializeField] private Image characterIcon;
    [SerializeField] private TMP_Text characterNameTxt;
    [SerializeField] private TMP_Text dialogueLineTxt;

    //La linea de dialogo que se debe mostrar
    private int currentlLine = 0;
    private void Start()
    {
        
    }

    void ShowDialogueLine()
    {
        //Actualizar el texto de la linea de dialogo
        dialogueLineTxt.text = currenDialogue.lines[currentlLine].text;
    }
}
