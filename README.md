# Secrets App
Simple console application to hold and manage you passwords hashed locally.

To use application:
1. Clone repository
2. Build Secrets.App project with command 'dotnet publish -c Release'
3. Add "{sln_directory}\Secrets\Secrets.App\bin\Release\net6.0\publish" to the PATH variables
4. Run terminal and use command 'secrets init'
5. App ready to user

App commands:
* secrets - show all secrets
* secrets [N] - show secret data by index 'N'
* secrets add [key] [login] [password] - add new secrets
* secrets rm [N] - remove key by number

TODO Next:
1. Refactor the cryptography project logic to make setup possible 
2. Refactor the getting default storage file logic, rid of hardcode in several places
3. Add 'help' command
4. Extend the project documentation