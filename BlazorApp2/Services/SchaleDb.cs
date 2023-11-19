using BlazorApp2.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp2.Services;

[JsonSerializable(typeof(EquipmentInfo[]))]
[JsonSerializable(typeof(StudentInfo[]))]
internal partial class SerializerContext : JsonSerializerContext { }

public class SchaleDb(HttpClient httpClient)
{
    private Task<EquipmentInfo[]>? equipmentItemsTask;

    public Task<EquipmentInfo[]> GetEquipmentItems()
    {
        return equipmentItemsTask ??= httpClient.GetFromJsonAsync("data/cn/equipment.json", SerializerContext.Default.EquipmentInfoArray)!;
    }

    private Task<StudentInfo[]>? studentInfosTask;
    
    public Task<StudentInfo[]> GetStudentInfos()
    {
        return studentInfosTask ??= httpClient.GetFromJsonAsync("data/cn/students.json", SerializerContext.Default.StudentInfoArray)!;
    }
}
