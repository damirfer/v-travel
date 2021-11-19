# eTravel

### Setup guide
- Run migrations to create necessary database
- Run scripts
- Open api.sln and change connstring inside appsetting.json so it matches your local tenantDb conn string
- Open tenantDb trough SSMS and in table Tenants change DatabaseConnectionString field so it matches your local eTravelDb conn string
- Restore nugets, do npm installs in all projects
- Happy coding!
