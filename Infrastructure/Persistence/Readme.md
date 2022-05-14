### Migration ###

### add ###
dotnet ef --startup-project ../HangfireApi migrations add init_db -o Persistence/Migrations -c ApplicationDbContext --verbose