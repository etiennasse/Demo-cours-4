using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField] public GameObject dialogPrefab;
    [SerializeField] public GameObject mainCanvas;

    private bool actionAxisInUSe = true;
    private GameObject player;
    private bool dialogIsInitialised = false;
    private DialogText currentDialog;
    private DialogDisplayer currentDialogDisplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void Update()
    {
        ProcessInput();
    }
    public void StartDialog(DialogText newDialog)
    {
        dialogIsInitialised = true;
        player.GetComponent<PlayerMovement>().DisableControl();
        currentDialog = newDialog;
        GameObject currentDialogObject = Instantiate(dialogPrefab, mainCanvas.transform);
        currentDialogDisplayer = currentDialogObject.GetComponent<DialogDisplayer>();
        currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
    }
    public void ProcessInput()
    {
        if (ShouldProcessInput())
        {
            actionAxisInUSe = true;
            if (currentDialog.IsNextDialog())
            {
                currentDialog = currentDialog.GetNextDialog();
                currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
            }
        }
    }
    public void EndDialog()
    {
        dialogIsInitialised = false;
        currentDialogDisplayer.CloseDialog();
        player.GetComponent<PlayerMovement>().EnableControl();
        currentDialog = null;
    }

     private bool ShouldProcessInput()
    {
        if (dialogIsInitialised)
        {
            if (!actionAxisInUSe && Input.GetAxis("Jump") !=0)
            {
                return true;
            }
        }
        return false;
    }

    private void ValideAxisInUse()
    {
        if(Input.GetAxis("Jump") !=0)
        {
            actionAxisInUSe = true;
        }
        else
        {
            actionAxisInUSe = false;
        }
    }

}
