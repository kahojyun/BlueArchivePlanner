using BlazorApp2.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp2.Services;

[JsonSerializable(typeof(EquipmentInfo[]))]
[JsonSerializable(typeof(StudentInfo[]))]
[JsonSerializable(typeof(StageInfo))]
[JsonSerializable(typeof(ConfigInfo))]
internal partial class SerializerContext : JsonSerializerContext { }

public class SchaleDb(HttpClient httpClient)
{
    private Task<Dictionary<int, EquipmentInfo>>? equipmentInfosTask;

    public Task<Dictionary<int, EquipmentInfo>> GetEquipmentInfos()
    {
        return equipmentInfosTask ??= GetEquipmentInfosImpl();

        async Task<Dictionary<int, EquipmentInfo>> GetEquipmentInfosImpl()
        {
            var data = await httpClient.GetFromJsonAsync("data/cn/equipment.min.json", SerializerContext.Default.EquipmentInfoArray);
            return data!.ToDictionary(x => x.Id);
        }
    }

    private Task<Dictionary<int, StudentInfo>>? studentInfosTask;
    
    public Task<Dictionary<int, StudentInfo>> GetStudentInfos()
    {
        return studentInfosTask ??= GetStudentInfosImpl();

        async Task<Dictionary<int, StudentInfo>> GetStudentInfosImpl()
        {
            var data = await httpClient.GetFromJsonAsync("data/cn/students.min.json", SerializerContext.Default.StudentInfoArray);
            return data!.ToDictionary(x => x.Id);
        }
    }

    private Task<Dictionary<int, Campaign>>? campaignTask;

    public Task<Dictionary<int, Campaign>> GetCampaigns()
    {
        return campaignTask ??= GetCampaignsImpl();

        async Task<Dictionary<int, Campaign>> GetCampaignsImpl()
        {
            var stageInfo = await httpClient.GetFromJsonAsync("data/stages.min.json", SerializerContext.Default.StageInfo);
            return stageInfo!.Campaign!.ToDictionary(x => x.Id);
        }
    }

    private Task<Dictionary<int, GachaGroup>>? gachaGroupsTask;

    public Task<Dictionary<int, GachaGroup>> GetGachaGroups()
    {
        return gachaGroupsTask ??= GetGachaGroupsImpl();

        async Task<Dictionary<int, GachaGroup>> GetGachaGroupsImpl()
        {
            var configInfo = await httpClient.GetFromJsonAsync("data/config.min.json", SerializerContext.Default.ConfigInfo);
            return configInfo!.GachaGroups!.ToDictionary(x => x.Id);
        }
    }
}
