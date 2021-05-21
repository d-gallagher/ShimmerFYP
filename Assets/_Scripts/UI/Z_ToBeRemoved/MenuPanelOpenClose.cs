using UnityEngine;
using UnityEngine.UI;

namespace UI.Z_ToBeRemoved
{
    public class MenuPanelOpenClose : MonoBehaviour
    {
        //public GameObject Panel;
        public Button BtnTogglePanel;

        private void TogglePanel()
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                bool isVisible = animator.GetBool("isVisible");
                animator.SetBool("isVisible", !isVisible);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            BtnTogglePanel.onClick.AddListener(BtnTogglePanel_onClick);
        }

        private void BtnTogglePanel_onClick()
        {
            TogglePanel();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
