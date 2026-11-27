using System.ComponentModel.DataAnnotations;
using AcxiomCRM.Models;
namespace AcxiomCRM.ViewModels;
public class CustomerInput { public string? AssignedTo {get;set;}
[Required, StringLength(150)] public string CustomerName {get;set;} ="";
[Required, EmailAddress, StringLength(254)] public string Email {get;set;} ="";
[Required, RegularExpression(@"^[0-9]{10}$", ErrorMessage="Enter a 10-digit phone number.")] public string Phone {get;set;} ="";
[StringLength(500)] public string? CompanyName {get;set;} ="";
[StringLength(500)] public string? Address {get;set;} ="";
[StringLength(500)] public string? City {get;set;} ="";
[StringLength(500)] public string? State {get;set;} ="";
[Required, RegularExpression("^(Active|Inactive)$")] public string Status {get;set;} ="Active";
[StringLength(500)] public string? Notes {get;set;} ="";
public void Apply(Customer e) {e.CustomerName=CustomerName;e.Email=Email;e.Phone=Phone;e.CompanyName=CompanyName??"";e.Address=Address??"";e.City=City??"";e.State=State??"";e.Status=Status;e.Notes=Notes??"";}
public static CustomerInput From(Customer e)=>new(){AssignedTo=e.AssignedTo,CustomerName=e.CustomerName,Email=e.Email,Phone=e.Phone,CompanyName=e.CompanyName,Address=e.Address,City=e.City,State=e.State,Status=e.Status,Notes=e.Notes};
}
public class LeadInput { public string? AssignedTo {get;set;}
[Required, StringLength(150)] public string LeadName {get;set;} ="";
[Required, EmailAddress, StringLength(254)] public string Email {get;set;} ="";
[Required, RegularExpression(@"^[0-9]{10}$", ErrorMessage="Enter a 10-digit phone number.")] public string Phone {get;set;} ="";
[StringLength(500)] public string? CompanyName {get;set;} ="";
[StringLength(500)] public string? Source {get;set;} ="";
[Required, RegularExpression("^(New|Contacted|Qualified|Unqualified|Converted|Lost)$")] public string Status {get;set;} ="New";
[Range(typeof(decimal), "0", "1000000000000")] public decimal ExpectedValue {get;set;} 
[Required, RegularExpression("^(Low|Medium|High)$")] public string Priority {get;set;} ="Low";
[StringLength(500)] public string? Notes {get;set;} ="";
public void Apply(Lead e) {e.LeadName=LeadName;e.Email=Email;e.Phone=Phone;e.CompanyName=CompanyName??"";e.Source=Source??"";e.Status=Status;e.ExpectedValue=ExpectedValue;e.Priority=Priority;e.Notes=Notes??"";}
public static LeadInput From(Lead e)=>new(){AssignedTo=e.AssignedTo,LeadName=e.LeadName,Email=e.Email,Phone=e.Phone,CompanyName=e.CompanyName,Source=e.Source,Status=e.Status,ExpectedValue=e.ExpectedValue,Priority=e.Priority,Notes=e.Notes};
}
public class OpportunityInput { public string? AssignedTo {get;set;}
[Required, StringLength(150)] public string OpportunityName {get;set;} ="";
[Required] public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
[Range(typeof(decimal), "0", "1000000000000")] public decimal Amount {get;set;} 
[Required, RegularExpression("^(Qualification|Proposal|Negotiation|Won|Lost)$")] public string Stage {get;set;} ="Qualification";
[Range(0,100)] public int Probability {get;set;} 
[DataType(DataType.Date)] public DateTime ExpectedCloseDate {get;set;} =DateTime.Today;
[StringLength(500)] public string? Source {get;set;} ="";
[StringLength(500)] public string? Notes {get;set;} ="";
public void Apply(Opportunity e) {e.OpportunityName=OpportunityName;e.CustomerId=CustomerId;e.LeadId=LeadId;e.Amount=Amount;e.Stage=Stage;e.Probability=Probability;e.ExpectedCloseDate=ExpectedCloseDate;e.Source=Source??"";e.Notes=Notes??"";}
public static OpportunityInput From(Opportunity e)=>new(){AssignedTo=e.AssignedTo,OpportunityName=e.OpportunityName,CustomerId=e.CustomerId,LeadId=e.LeadId,Amount=e.Amount,Stage=e.Stage,Probability=e.Probability,ExpectedCloseDate=e.ExpectedCloseDate,Source=e.Source,Notes=e.Notes};
}
public class FollowUpInput { public string? AssignedTo {get;set;}
[Required, StringLength(150)] public string Subject {get;set;} ="";
 public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
 public int? OpportunityId {get;set;} 
[DataType(DataType.Date)] public DateTime FollowUpDate {get;set;} =DateTime.Today;
[Required, RegularExpression("^(Call|Meeting|Email|Task)$")] public string FollowUpType {get;set;} ="Call";
[StringLength(500)] public string? Remarks {get;set;} ="";
[Required, RegularExpression("^(Planned|Completed|Missed|Cancelled)$")] public string Status {get;set;} ="Planned";
public void Apply(FollowUp e) {e.Subject=Subject;e.CustomerId=CustomerId;e.LeadId=LeadId;e.OpportunityId=OpportunityId;e.FollowUpDate=FollowUpDate;e.FollowUpType=FollowUpType;e.Remarks=Remarks??"";e.Status=Status;}
public static FollowUpInput From(FollowUp e)=>new(){AssignedTo=e.AssignedTo,Subject=e.Subject,CustomerId=e.CustomerId,LeadId=e.LeadId,OpportunityId=e.OpportunityId,FollowUpDate=e.FollowUpDate,FollowUpType=e.FollowUpType,Remarks=e.Remarks,Status=e.Status};
}
public class ActivityInput { public string? AssignedTo {get;set;}
[Required, RegularExpression("^(Call|Meeting|Email|Task)$")] public string ActivityType {get;set;} ="Call";
[Required, StringLength(150)] public string Subject {get;set;} ="";
[StringLength(500)] public string? Description {get;set;} ="";
[DataType(DataType.Date)] public DateTime ActivityDate {get;set;} =DateTime.Today;
 public int? CustomerId {get;set;} 
 public int? LeadId {get;set;} 
[Required, RegularExpression("^(Planned|Completed|Cancelled)$")] public string Status {get;set;} ="Planned";
public void Apply(Activity e) {e.ActivityType=ActivityType;e.Subject=Subject;e.Description=Description??"";e.ActivityDate=ActivityDate;e.CustomerId=CustomerId;e.LeadId=LeadId;e.Status=Status;}
public static ActivityInput From(Activity e)=>new(){AssignedTo=e.AssignedTo,ActivityType=e.ActivityType,Subject=e.Subject,Description=e.Description,ActivityDate=e.ActivityDate,CustomerId=e.CustomerId,LeadId=e.LeadId,Status=e.Status};
}
