# SoCal Code Camp - Prism Auth Demo

This is the sample repo for the Azure Active Directory Auth Module from the Prism for Xamarin Forms Deep Dive talk. This sample shows an example of how you can compartmentalize development of specific features within your application in isolation from the larger app with an ability to package the Module for consumption in one or more applications.

| Build Status | NuGet |
|:------------:|:-----:|
| [![Build Status](https://dev.azure.com/dansiegel/SoCal%20Code%20Camp/_apis/build/status/AuthModule-CI)](https://dev.azure.com/dansiegel/SoCal%20Code%20Camp/_build/latest?definitionId=32) | [![Build Status](https://img.shields.io/nuget/vpre/SoCalCodeCamp.AuthDemo.svg)](https://nuget.org/packages/SoCalCodeCamp.AuthDemo) |

## Building the Sample App

The sample app includes the Mobile.BuildTools library for injecting secrets. You will need to setup an Azure Active Directory B2C instance and add the appropriate values to a secrets.json file. Note the file is intentionally excluded from Source Control. You will need to create it in the SoCalCodeCamp.AuthDemo.Sample project.

```json
{
  "TenantName": "fabrikamb2c",
  "ClientId": "90c0fe63-bcf2-44d5-8fb7-b8bbc0b29dc6",
  "Scopes": "https://fabrikamb2c.onmicrosoft.com/helloapi/demo.read",
  "Policy": "b2c_1_susi"
}
```

## Updating iOS and Android Projects

The AndroidManifest.xml and Info.plist each have explicit references to the ClientId. Before building the project be sure to update the field {REPLACEME} with the Client ID that you use in the secrets.json.
