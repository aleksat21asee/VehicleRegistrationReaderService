# Vehicle Registration Reader Service

Vehicle Registration Reader Service is a wrapper around eVehicleRegistrationAPI provided by Republic of Serbia, Ministry of Interior.
 
## Running Vehicle Registration Reader Service

Vehicle Registration Reader Service can be hosted on IIS or run as a Windows service.

### Hosting on IIS

Launch host and port can be configured in ``IdCardReaderService\launchSettings.json``.

e.g.

```
"iisExpress": {
    "applicationUrl": "http://localhost:23784"
}
```
## Install as Windows service

1. Download ``VehicleRegistrationReaderRoot.7z`` from the latest release.
2. Extract files to desired location where the application will be running.
3. Run file ``install.exe`` as **administartor**.

## Uninstall as Windows service
1. Go to the **root** folder of the application.
2. Run file ``uninstall.exe`` as **administartor**.

## How to use
There are currently 4 available endpoints:

1. #### GetReaderNames 
http://localhost:61408/v1/vehicle-registration-card-reader/reader-names
```
{
    "cardReaders": [
        {
            readerName: string
        }
    ]
}
```
2. #### GetPersonalData 
http://localhost:61408/v1/vehicle-registration-card-reader/personal-data?$readerName
```
{
    ownersPersonalNo: string,
    ownersSurnameOrBusinessName: string,
    ownerName: string,
    ownerAddress: string,
    usersPersonalNo: string,
    usersSurnameOrBusinessName: string,
    usersName: string,
    usersAddress: string
}
```
3. #### GetDocumentData
http://localhost:61408/v1/vehicle-registration-card-reader/document-data?$readerName
```
{
    stateIssuing: string,
    competentAuthority: string,
    authorityIssuing: strign,
    unambiguousNumber: string,
    issuingDate: string,
    expiryDate: string,
    serialNumber: string
}
```
4. #### GetVehicleData 
http://localhost:61408/v1/vehicle-registration-card-reader/vehicle-data?$readerName
```
{
    dateOfFirstRegistration: string,
    yearOfProduction: string,
    vehicleMake: string,
    vehicleType: string,
    commercialDescription: string,
    vehicleIDNumber: string,
    registrationNumberOfVehicle: string,
    maximumNetPower: string,
    engineCapacity: string,
    typeOfFuel: string,
    powerWeightRatio: string,
    vehicleMass: string,
    maximumPermissibleLadenMass: string,
    typeApprovalNumber: string,
    numberOfSeats: string,
    numberOfStandingPlaces: string,
    engineIDNumber: string,
    numberOfAxles: string,
    vehicleCategory: string,
    colourOfVehicle: string,
    restrictionToChangeOwner: string,
    vehicleLoad: string
}
```
### Note
Detailed description of property attributes can be found in ``eVehicleRegistrationAPI/eVehicle Registration SDK Korisnicko Uputstvo.pdf``
