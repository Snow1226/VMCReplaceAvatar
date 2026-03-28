using VMCReplaceAvatar.Osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VMC;
using System.Runtime.InteropServices;

namespace VMCReplaceAvatar
{
    public class FloorOffset : MonoBehaviour
    {
        private GameObject _scaleSyncTarget;
        private float _currentScale;

        public Config Config;

        internal List<SendTask> sendTasks = new List<SendTask>();

        internal class SendTask
        {
            internal float offset = 0f;
            internal int port = 0;
            internal OscClient client = null;
        }

        private void Start()
        {
            _scaleSyncTarget = GameObject.Find("HandTrackerRoot");
            _currentScale = _scaleSyncTarget.transform.localScale.y;

            VMCEvents.OnModelLoaded += OnModelLoaded;

        }

        private void OnDestroy()
        {
            RemoveTask(Config.FloorOffsetPort);
        }

        private void OnModelLoaded(GameObject model)
        {
            var anim = model.GetComponent<Animator>();
            if (anim != null)
            {
                AddSendTask();
            }
        }

        public void AddSendTask()
        {
            SendTask sendTask = new SendTask();
            sendTask.offset = FloorOffsetCalculate();
            sendTask.port = Config.FloorOffsetPort;
            sendTask.client = new OscClient(Config.FloorOffsetSenderAddress, Config.FloorOffsetPort);
            if (sendTask.client != null)
            {
                sendTasks.Add(sendTask);
            }
            else
                Debug.LogError($"Instance of OscClient Not Starting.");
        }

        internal void RemoveTask(int port)
        {
            foreach (SendTask sendTask in sendTasks)
            {
                if (sendTask.port == port)
                {
                    sendTasks.Remove(sendTask);
                    break;
                }
            }
        }

        private async Task SendData()
        {
            await Task.Run(() => {
                try
                {
                    foreach (SendTask sendTask in sendTasks)
                    {
                        sendTask.client.Send("/VMC/Ext/Floor", "Floor", new float[] {
                            sendTask.offset
                        });
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"ExternalSender Thread : {e}");
                }
            });
        }

        private float FloorOffsetCalculate()
        {
            var scale = new Vector3(1.0f / _scaleSyncTarget.transform.localScale.x, 1.0f / _scaleSyncTarget.transform.localScale.y, 1.0f / _scaleSyncTarget.transform.localScale.z) ;
            return -Vector3.Scale(scale, _scaleSyncTarget.transform.localPosition).y;
        }

        private void LateUpdate()
        {
            if (_scaleSyncTarget != null)
            {
                if (_currentScale != _scaleSyncTarget.transform.localScale.y)
                {
                    _currentScale = _scaleSyncTarget.transform.localScale.y;

                    sendTasks.ForEach((sendTask) => {
                        sendTask.offset = FloorOffsetCalculate();
                    });
                    Debug.Log($"Scale Changed : {FloorOffsetCalculate()}");
                }
            }

            Task.Run(() => SendData());
        }
    }
}
