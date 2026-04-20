using Newtonsoft.Json;
using System.Collections.Generic;

namespace VMCReplaceAvatar
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Config
    {
        [JsonProperty] public bool DisplayUIatStartup = true;
        [JsonProperty] public bool avatarSelfScaling = false;
        [JsonProperty] public string FloorOffsetSenderAddress = "127.0.0.1";
        [JsonProperty] public int FloorOffsetPort = 39740;
        [JsonProperty] public int LightReceivePort = 39840;
        [JsonProperty] public List<VRMAvatarMeshSetting> vrmAvatarMeshSettings = new List<VRMAvatarMeshSetting>();
        [JsonProperty] public List<AvatarFloorOffset> avatarFloorOffsets = new List<AvatarFloorOffset>();
    }

    public class VRMAvatarMeshSetting
    {
        [JsonProperty] public string avatarName;
        [JsonProperty] public bool ignoreSyncInitialValue;
        [JsonProperty] public List<MeshSetting> meshSettings = new List<MeshSetting>();
    }

    public class MeshSetting
    {
        [JsonProperty] public string meshName;
        [JsonProperty] public bool isSync = false;
    }

    public class AvatarFloorOffset
    {
        [JsonProperty] public string avatarName;
        [JsonProperty] public float offset = 0f;
    }
}
