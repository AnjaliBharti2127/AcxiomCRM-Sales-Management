using AcxiomCRM.Data;using AcxiomCRM.Models;using Microsoft.EntityFrameworkCore;using System.Security.Claims;
namespace AcxiomCRM.Services;
public class CrmService(CrmDb db,IHttpContextAccessor http){
public ClaimsPrincipal User=>http.HttpContext!.User;public string Id=>User.FindFirstValue(ClaimTypes.NameIdentifier)!;
public IQueryable<ApplicationUser> Owners()=>db.Users.Where(x=>x.IsActive&&(User.IsInRole("Admin")||x.Id==Id||(User.IsInRole("Manager")&&x.ManagerId==Id)));
public IQueryable<T> Scope<T>() where T:class,IOwned {var q=db.Set<T>().Where(x=>!x.IsDeleted);if(User.IsInRole("Admin"))return q;var ids=Owners().Select(x=>x.Id);return q.Where(x=>ids.Contains(x.AssignedTo));}
public async Task<bool> CanAssign(string owner)=>await Owners().AnyAsync(x=>x.Id==owner);
public async Task<string?> Validate<T>(T row,string? oldStatus=null) where T:class,IOwned {
if(!await CanAssign(row.AssignedTo))return "Invalid or unauthorized owner.";
if(row is Customer c){c.Email=c.Email.Trim();c.NormalizedEmail=c.Email.ToUpperInvariant();if(await db.Customers.AnyAsync(x=>x.Id!=c.Id&&(x.NormalizedEmail==c.NormalizedEmail||x.Phone==c.Phone)))return "Email or phone already belongs to a customer.";}
if(row is Lead l){if(l.Status=="Converted"&&oldStatus!="Converted")return "Use the Convert action.";if(oldStatus==null&&l.Status!="New")return "New leads must start at New.";if(oldStatus!=null&&oldStatus!=l.Status&&!AllowedLead(oldStatus,l.Status))return "Invalid lead status transition.";}
if(row is Opportunity o){o.Status=o.Stage is "Won" or "Lost"?o.Stage:"Open";if(o.Amount<0||(o.Status=="Open"&&o.Amount<=0))return "Active opportunity amount must be greater than zero.";if(o.Probability<0||o.Probability>100)return "Probability must be 0–100.";if(o.Status=="Open"&&o.ExpectedCloseDate.Date<DateTime.Today)return "Expected close date cannot be in the past.";if(!await Related<Customer>(o.CustomerId)||!await Related<Lead>(o.LeadId))return "Related record unavailable.";o.ActualCloseDate=o.Status=="Open"?null:o.ActualCloseDate??DateTime.Today;}
if(row is FollowUp f){if((f.Id==0||f.Status=="Planned")&&f.FollowUpDate.Date<DateTime.Today)return "Follow-up date cannot be earlier than today.";if(f.CustomerId==null&&f.LeadId==null&&f.OpportunityId==null)return "Select a related record.";if(!await Related<Customer>(f.CustomerId)||!await Related<Lead>(f.LeadId)||!await Related<Opportunity>(f.OpportunityId))return "Related record unavailable.";}
if(row is Activity a){if(!await Related<Customer>(a.CustomerId)||!await Related<Lead>(a.LeadId))return "Related record unavailable.";}
return null;}
async Task<bool> Related<T>(int? id) where T:class,IOwned =>id==null||await Scope<T>().AnyAsync(x=>x.Id==id);
public static bool AllowedLead(string from,string to)=>from==to|| (from,to) is ("New","Contacted") or ("Contacted","Qualified") or ("Contacted","Unqualified") or ("New","Lost") or ("Contacted","Lost") or ("Qualified","Lost");
}
