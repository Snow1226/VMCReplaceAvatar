using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using VMCReplaceAvatar.Osc;

namespace VMCReplaceAvatar.LightManager
{
    public class LightManager : MonoBehaviour
    {
        private OscServer _oscServer;
        public Config Config;

        private Light _directionalLight;
        public Light LeftSaberLight;
        public Light RightSaberLight;

        private List<Color> _saberColors = new List<Color>() { Color.white, Color.white};
        private List<Vector3> _saberPos = new List<Vector3>() { Vector3.zero, Vector3.zero};
        private List<Quaternion> _saberRot = new List<Quaternion>() { Quaternion.identity, Quaternion.identity};

        private void Start()
        {
            _directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();

            var left = new GameObject("LeftSaberLight");
            left.transform.SetParent(this.transform);
            LeftSaberLight = left.AddComponent<Light>();
            LeftSaberLight.type = UnityEngine.LightType.Point;
            LeftSaberLight.range = Config.SaberLightRange;
            LeftSaberLight.intensity = Config.SaberLightIntensity;
            LeftSaberLight.shadows = LightShadows.Soft;

            var right = new GameObject("RightSaberLight");
            right.transform.SetParent(this.transform);
            RightSaberLight = right.AddComponent<Light>();
            RightSaberLight.type = UnityEngine.LightType.Point;
            RightSaberLight.range = Config.SaberLightRange;
            RightSaberLight.intensity = Config.SaberLightIntensity;
            RightSaberLight.shadows = LightShadows.Soft;
            Initialize();
        }

        private void OnDestroy()
        {
            if (_oscServer != null)
            {
                _oscServer.Dispose();
                _oscServer = null;
            }
        }

        private void Update()
        {
            LeftSaberLight.transform.localPosition = _saberPos[0];
            LeftSaberLight.transform.localRotation = _saberRot[0];
            LeftSaberLight.color = _saberColors[0];
            RightSaberLight.transform.localPosition = _saberPos[1];
            RightSaberLight.transform.localRotation = _saberRot[1];
            RightSaberLight.color = _saberColors[1];
        }

        public void Initialize()
        {
            if (Config == null) return;

            try
            {
                _oscServer = new OscServer(Config.LightReceivePort);

                _oscServer.MessageDispatcher.AddCallback(
                    "/VMC/Ext/DirectionalLight",
                    (string address, OscDataHandle data) =>
                    {
                        _directionalLight.color = new Color(data.GetElementAsFloat(8), data.GetElementAsFloat(9), data.GetElementAsFloat(10), data.GetElementAsFloat(11));
                    }
                );
                _oscServer.MessageDispatcher.AddCallback(
                    "/VMC/Ext/LeftSaber",
                    (string address, OscDataHandle data) =>
                    {
                        _saberPos[0] = new Vector3(data.GetElementAsFloat(1), data.GetElementAsFloat(2), data.GetElementAsFloat(3));
                        _saberRot[0] = new Quaternion(data.GetElementAsFloat(4), data.GetElementAsFloat(5), data.GetElementAsFloat(6), data.GetElementAsFloat(7));
                        _saberColors[0] = new Color(data.GetElementAsFloat(8), data.GetElementAsFloat(9), data.GetElementAsFloat(10), data.GetElementAsFloat(11));
                    }
                );
                _oscServer.MessageDispatcher.AddCallback(
                    "/VMC/Ext/RightSaber",
                    (string address, OscDataHandle data) =>
                    {
                        _saberPos[1] = new Vector3(data.GetElementAsFloat(1), data.GetElementAsFloat(2), data.GetElementAsFloat(3));
                        _saberRot[1] = new Quaternion(data.GetElementAsFloat(4), data.GetElementAsFloat(5), data.GetElementAsFloat(6), data.GetElementAsFloat(7));
                        _saberColors[1] = new Color(data.GetElementAsFloat(8), data.GetElementAsFloat(9), data.GetElementAsFloat(10), data.GetElementAsFloat(11));
                    }
                );

            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize OscServer: {e.Message}");
            }
        }
    }

}
