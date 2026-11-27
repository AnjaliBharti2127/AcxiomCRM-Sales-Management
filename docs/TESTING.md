# Local acceptance tests (not yet executed)
1. Run setup.ps1; check compilation and migration output.
2. Open Dashboard in private browser: expect redirect to login. GET /api/customers without cookie: expect 401.
3. Register a new user; confirm SalesExecutive only. Try five incorrect passwords; check lockout.
4. Create a customer with empty name, invalid email/phone and duplicate contact: expect rejection. Repeat via API with CSRF token.
5. Create lead New, edit to Contacted, then Qualified, then Convert. Check Customer/Opportunity and audit log; retry conversion.
6. Create active opportunity with zero amount, 101 probability and past close date: expect rejection.
7. Create new follow-up in past: expect rejection. Complete an existing follow-up.
8. Sign in as sales1 and sales2 separately; test changing IDs and owner assignments. Manager1 must not see manager2 team. Admin sees all records.
9. Verify all dashboard counts against visible records and all charts render. Verify reports and CSV data respect role scope.
10. Create/update/delete CRM records and inspect audit entries. Deactivate a user while signed in and verify further access is denied.
11. Run dotnet test AcxiomCRM.Tests. Unit tests cover basic transitions and annotations; integration and browser tests are manual and outstanding.
