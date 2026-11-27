using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace AcxiomCRM.Models;
public class ApplicationUser:IdentityUser {public string FullName {get;set;}="";public bool IsActive {get;set;}=true;public string? ManagerId {get;set;} public DateTime CreatedDate {get;set;}=DateTime.UtcNow;}
public interface IOwned {int Id {get;set;} string AssignedTo {get;set;} DateTime CreatedDate {get;set;} bool IsDeleted {get;set;}}
public class AuditLog {public int AuditLogId {get;set;} public string? UserId {get;set;} public string Action {get;set;}="";public string EntityName {get;set;}="";public string RecordId {get;set;}="";public string? OldValue {get;set;} public string? NewValue {get;set;} public DateTime CreatedDate {get;set;}=DateTime.UtcNow;public string? IpAddress {get;set;} public string Result {get;set;}="Success";}
public class Customer:IOwned { [Column("CustomerId")] public int Id {get;set;} public string AssignedTo {get;set;}=""; public DateTime CreatedDate {get;set;}=DateTime.UtcNow; public bool IsDeleted {get;set;}
[Required, StringLength(150)] public string CustomerName {get;set;} ="";
[Required, EmailAddress, StringLength(254)] public string Email {get;set;} ="";
[Required, RegularExpression(@"^[0-9]{10}$", ErrorMessage="Enter a 10-digit phone number.")] public string Phone {get;set;} ="";
[StringLength(500)] public string CompanyName {get;set;} ="";
[StringLength(500)] public string Address {get;set;} ="";
[StringLength(500)] public string City {get;set;} ="";
[StringLength(500)] public string State {get;set;} ="";
 public string Status {get;set;} ="Active";
[StringLength(500)] public string Notes {get;set;} ="";
public string CustomerCode {get;set;}=Guid.NewGuid().ToString("N");
public string CreatedBy {get;set;}=""; public string NormalizedEmail {get;set;}="";public DateTime ModifiedDate {get;set;}=DateTime.UtcNow;
}
public class Lead:IOwned { [Column("LeadId")] public int Id {get;set;} public string AssignedTo {get;set;}=""; public DateTime CreatedDate {get;set;}=DateTime.UtcNow; public bool IsDeleted {get;set;}
[Required, StringLength(150)] public string LeadName {get;set;} ="";
[Required, EmailAddress, StringLength(254)] public string Email {get;set;} ="";
[Required, RegularExpression(@"^[0-9]{10}$", ErrorMessage="Enter a 10-digit phone number.")] public string Phone {get;set;} ="";
[StringLength(500)] public string CompanyName {get;set;} ="";
[StringLength(500)] public string Source {get;set;} ="";
 public string Status {get;set;} ="New";
[Range(typeof(decimal), "0", "1000000000000")] public decimal ExpectedValue {get;set;} 
 public string Priority {get;set;} ="Low";
[StringLength(500)] public string Notes {get;set;} ="";
public string LeadCode {get;set;}=Guid.NewGuid().ToString("N");
public int? ConvertedCustomerId {get;set;}
}
public class Opportunity:IOwned { [Column("OpportunityId")] public int Id {get;set;} public string AssignedTo {get;set;}=""; public DateTime CreatedDate {get;set;}=DateTime.UtcNow; public bool IsDeleted {get;set;}
[Required, StringLength(150)] public string OpportunityName {get;set;} ="";
[Required] public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
[Range(typeof(decimal), "0", "1000000000000")] public decimal Amount {get;set;} 
 public string Stage {get;set;} ="Qualification";
[Range(0,100)] public int Probability {get;set;} 
[DataType(DataType.Date)] public DateTime ExpectedCloseDate {get;set;} =DateTime.Today;
[StringLength(500)] public string Source {get;set;} ="";
[StringLength(500)] public string Notes {get;set;} ="";
public string Status {get;set;}="Open";public DateTime? ActualCloseDate {get;set;}
}
public class FollowUp:IOwned { [Column("FollowUpId")] public int Id {get;set;} public string AssignedTo {get;set;}=""; public DateTime CreatedDate {get;set;}=DateTime.UtcNow; public bool IsDeleted {get;set;}
[Required, StringLength(150)] public string Subject {get;set;} ="";
 public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
 public int? OpportunityId {get;set;} 
[DataType(DataType.Date)] public DateTime FollowUpDate {get;set;} =DateTime.Today;
 public string FollowUpType {get;set;} ="Call";
[StringLength(500)] public string Remarks {get;set;} ="";
 public string Status {get;set;} ="Planned";
}
public class Activity:IOwned { [Column("ActivityId")] public int Id {get;set;} public string AssignedTo {get;set;}=""; public DateTime CreatedDate {get;set;}=DateTime.UtcNow; public bool IsDeleted {get;set;}
 public string ActivityType {get;set;} ="Call";
[Required, StringLength(150)] public string Subject {get;set;} ="";
[StringLength(500)] public string Description {get;set;} ="";
[DataType(DataType.Date)] public DateTime ActivityDate {get;set;} =DateTime.Today;
 public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
 public string Status {get;set;} ="Planned";
}
