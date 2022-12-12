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

a) Install with .exe file
1. Download ``VehicleRegistrationReaderRoot.7z`` file from the latest release.
2. Extract files to desired location where the application will be running.
3. Run file ``install.exe`` as **administrator**.
4. Go through installation steps that are shown in the console. \
**Note** - During the installation you might get a warning to install a certificate from vrcardreader.asseco-see.rs , click **Yes** to accept and install the certificate.
5. After the installation is complete, you will have working windows service (VehicleRegistrationCardReaderService) running on your machine on both http and https on the ports which you selected during installation.

b) Install with powershell script
1. Download ``VehicleRegistrationReaderRoot.7z`` file from the latest release.
2. Extract files to desired location where the application will be running.
3. Open powershell as **administrator**
4. Position to the installation folder
5. Run ``.\install.ps1`` \
**Note** - During the installation you might get a warning to install a certificate from vrcardreader.asseco-see.rs , click **Yes** to accept and install the certificate.
6. After the installation is complete, you will have working windows service (VehicleRegistrationCardReaderService) running on your machine on both http and https on the ports which you selected during installation. \
\
**Note** - Ports can be configured by running script with appropriate arguments: ```.\install.ps1 <port number> <ssl port number>``` . Default ports are 61408 for HTTP and 5002 for HTTPS.

## Uninstall as Windows service

a) Uninstall with .exe file
1. Go to the **root** folder of the application.
2. Run file ``uninstall.exe`` as **administrator**.

b) Uninstall with powershell script
1. Open powershell as **administrator**
2. Position to the installation folder
3. Run ``.\uninstall.ps1``

## How to use
Full API specification can be found in api-spec folder. To see the list of available methods and data models, you will need to open .yaml file in editor like swagger. 
```
https://editor.swagger.io/
```
### Note
Detailed description of property attributes can also be found in ``eVehicleRegistrationAPI/eVehicle Registration SDK Korisnicko Uputstvo.pdf``
