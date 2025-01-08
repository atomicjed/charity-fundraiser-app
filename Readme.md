### Development account
To setup your AWS SSO; please follow the instructions below.
Run the command:
```
   aws configure sso --profile sfs-dev
```
When prompted enter the following values:
```
   SSO session name (Recommended): sfs-dev-session
   AWS SSO start URL [None]: https://d-9c676c367e.awsapps.com/start/#
   AWS SSO region [None]: eu-west-2
   SSO registration scopes [sso:account:access]: [USE DEFAULT - Just press enter]
```



### Create a Migration
```
   export SsmParameterStoreRoot=/fundraiser-app
   dotnet ef migrations add [*Migration Name*] --project ../CharityFundraiserApp.ApplicationDatabase
```

### Apply the Migration
```
   export SsmParameterStoreRoot=/fundraiser-app
   dotnet ef database update --project ../CharityFundraiserApp.ApplicationDatabase 
```
