# You must run this script in administrator mode

$serviceName = 'VehicleRegistrationReaderService'

# check if service exists
If(Get-Service -Name $serviceName -ErrorAction SilentlyContinue)
{
	# stop service
	write-host 'Stopping service '$serviceName' ...'
	Set-Service -Name $serviceName -Status Stopped -ErrorAction Stop

	# uninstall service
	write-host 'Uninstalling service '$serviceName' ...'
	sc.exe delete $serviceName
	
	write-host 'Service '$serviceName' has been uninstalled! You may need to restart your computer for this action to take effect.'
}
Else
{
	write-host 'Service '$serviceName' is not installed!'
}