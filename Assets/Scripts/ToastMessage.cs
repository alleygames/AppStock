using System.Collections;
using TMPro;
using UnityEngine;

public class ToastMessage : MonoBehaviour
{
    public static ToastMessage instance;
    [SerializeField] private TMP_Text messageText;

    private Coroutine _messageCoroutine;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMessage(string message)
    {
        if (_messageCoroutine != null)
        {
            StopCoroutine(_messageCoroutine);
            messageText.text = "";
        }
        
        _messageCoroutine = StartCoroutine(Message(message));
    }

    private IEnumerator Message(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(2);
        messageText.text = "";
    }
}
