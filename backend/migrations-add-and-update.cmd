dotnet-ef database drop -f -c AccountsDbContext -p .\src\Accounts\AnimalAllies.Accounts.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database drop -f -c VolunteerWriteDbContext -p .\src\PetManagement\AnimalAllies.Volunteer.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database drop -f -c SpeciesWriteDbContext -p .\src\BreedManagement\AnimalAllies.Species.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database drop -f -c WriteDbContext -p .\src\VolunteerRequests\VolunteerRequests.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database drop -f -c WriteDbContext -p .\src\Discussion\Discussion.Infrastructure\ -s .\src\AnimalAllies.Web\


dotnet-ef migrations remove -c AccountsDbContext -p .\src\Accounts\AnimalAllies.Accounts.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations remove -c VolunteerWriteDbContext -p .\src\PetManagement\AnimalAllies.Volunteer.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations remove -c SpeciesWriteDbContext -p .\src\BreedManagement\AnimalAllies.Species.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations remove -c WriteDbContext -p .\src\VolunteerRequests\VolunteerRequests.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations remove -c WriteDbContext -p .\src\Discussion\Discussion.Infrastructure\ -s .\src\AnimalAllies.Web\


dotnet-ef migrations add init -c AccountsDbContext -p .\src\Accounts\AnimalAllies.Accounts.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations add init -c VolunteerWriteDbContext -p .\src\PetManagement\AnimalAllies.Volunteer.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations add init -c SpeciesWriteDbContext -p .\src\BreedManagement\AnimalAllies.Species.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations add init -c WriteDbContext -p .\src\VolunteerRequests\VolunteerRequests.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef migrations add init -c WriteDbContext -p .\src\Discussion\Discussion.Infrastructure\ -s .\src\AnimalAllies.Web\

dotnet-ef database update -c AccountsDbContext -p .\src\Accounts\AnimalAllies.Accounts.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database update -c VolunteerWriteDbContext -p .\src\PetManagement\AnimalAllies.Volunteer.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database update -c SpeciesWriteDbContext -p .\src\BreedManagement\AnimalAllies.Species.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database update -c WriteDbContext -p .\src\VolunteerRequests\VolunteerRequests.Infrastructure\ -s .\src\AnimalAllies.Web\
dotnet-ef database update -c WriteDbContext -p .\src\Discussion\Discussion.Infrastructure\ -s .\src\AnimalAllies.Web\