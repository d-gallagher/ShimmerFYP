using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PanelPlayers
{
    public class PanelSelectedPlayerScript : MonoBehaviour
    {
        private PanelPlayerTrainingRecordsScript _panelPlayerTrainingRecordsScript;

        private UnityPlayerModel _model;

        public InputField TxtId;
        public InputField TxtFirstName;
        public InputField TxtLastName;

        private void Start()
        {
            _panelPlayerTrainingRecordsScript = FindObjectOfType<PanelPlayerTrainingRecordsScript>();
        }

        public void SetPlayer(UnityPlayerModel player)
        {
            this._model = player;

            TxtId.text = _model.Id.ToString();
            TxtFirstName.text = _model.FirstName;
            TxtLastName.text = _model.LastName;

            _panelPlayerTrainingRecordsScript.SetPlayerId(_model.Id);
        }
    }
}
