using AcxiomCRM.Data;using AcxiomCRM.Models;using System.Security.Claims;using System.Text.Json;
namespace AcxiomCRM.Services;
public class AuditService(CrmDb db,IHttpContextAccessor http){
public void Add(string action,string entity,string id,object? before=null,object? after=null,string result="Success",string? userId=null){db.AuditLogs.Add(new(){Action=action,EntityName=entity,RecordId=id,OldValue=before==null?null:JsonSerializer.Serialize(before),NewValue=after==null?null:JsonSerializer.Serialize(after),Result=result,UserId=userId??http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),IpAddress=http.HttpContext?.Connection.RemoteIpAddress?.ToString()});}
public async Task Write(string action,string entity,string id,string result="Success",string? userId=null){Add(action,entity,id,result:result,userId:userId);await db.SaveChangesAsync();}}
