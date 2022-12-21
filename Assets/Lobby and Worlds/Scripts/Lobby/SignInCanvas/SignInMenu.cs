using FirstGearGames.LobbyAndWorld.Extensions;
using FirstGearGames.LobbyAndWorld.Global;
using FirstGearGames.LobbyAndWorld.Global.Canvases;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace FirstGearGames.LobbyAndWorld.Lobbies.SignInCanvases
{

    public class SignInMenu : MonoBehaviour
    {
        /// <summary>
        /// Input field to disable when trying to login.
        /// </summary>
        [Tooltip("Input field to disable when trying to login.")]
        [SerializeField]
        private TMP_InputField _usernameText;
        /// <summary>
        /// SignIn button the user interacts with.
        /// </summary>
        [Tooltip("SignIn button the user interacts with.")]
        [SerializeField]
        private Button _signInButton;

        /// <summary>
        /// CanvasGroup on this object.
        /// </summary>
        private CanvasGroup _canvasGroup;
        /// <summary>
        /// SignInCanvas reference.
        /// </summary>
        private SignInCanvas _signInCanvas;

        public void FirstInitialize(SignInCanvas signInCanvas)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _signInCanvas = signInCanvas;
            Reset();
        }

        /// <summary>
        /// Resets canvases as though first login.
        /// </summary>
        public void Reset()
        {
            SignInFailed(string.Empty);
        }

        /// <summary>
        /// Called when SignIn button is pressed.
        /// </summary>
        public void OnClick_SignIn()
        {
            string username = _usernameText.text.Trim();
            string failedReason = string.Empty;            
            //Local sanitization failed.
            if (!LobbyNetwork.SanitizeUsername(ref username, ref failedReason))
            {
                GlobalManager.CanvasesManager.MessagesCanvas.InfoMessages.ShowTimedMessage(failedReason, MessagesCanvas.BRIGHT_ORANGE);
            }
            //Try to login through server.
            else
            {
                //Invoke(nameof(SignIn), 0f);
                SignIn();
            }
        }

        void SignIn()
        {
            string username = _usernameText.text.Trim();
            SetSignInLocked(true);
            LobbyNetwork.SignIn(username);
        }

        public void OnClick_Quit(Animator anim)
        {
            anim.SetTrigger("show");
            Invoke(nameof(QuitGame), 1.5f);
        }

        void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Sets locked state for signing in.
        /// </summary>
        /// <param name="locked"></param>
        private void SetSignInLocked(bool locked)
        {
            _signInButton.interactable = !locked;
            _usernameText.enabled = !locked;
        }

        /// <summary>
        /// Called after successfully signing in.
        /// </summary>
        public IEnumerator SignInSuccess()
        {
            SetSignInLocked(false);
            yield return new WaitForSeconds(0.5f);
            _canvasGroup.SetActive(false, true);
        }

        /// <summary>
        /// Called after failing to sign in.
        /// </summary>
        public void SignInFailed(string failedReason)
        {
            _canvasGroup.SetActive(true, true);
            SetSignInLocked(false);

            if (failedReason != string.Empty)
                GlobalManager.CanvasesManager.MessagesCanvas.InfoMessages.ShowTimedMessage(failedReason, Color.red);
        }
    }

}