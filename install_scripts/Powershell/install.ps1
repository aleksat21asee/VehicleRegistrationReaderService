# You must run this script in administrator mode

$projectName = 'VehicleRegistrationReaderService'
$serviceName = 'VehicleRegistrationReaderService'

$certHost = 'vrcardreader.asseco-see.rs'
$certName = 'vrcardreader.pfx'
$certPassword = 'rs.asseco-see.vrcardreader'

# get arguments
$portNumber = $args[1]
$portNumberSsl = $args[2]

# get current directory
$currentDirectory = Get-Location
$solutionPath = $currentDirectory


If($portNumber)
{
	$portNumber = "--port-number $portNumber"
}

If($portNumberSsl) 
{
	$portNumberSsl = "--port-number-ssl $portNumberSsl"
}

$hostsFilePath = "$($Env:WinDir)\system32\Drivers\etc\hosts"
$hostsFileContent = Get-Content $hostsFilePath

# write new entry to hosts file
$desiredIp = "127.0.0.1"

$escapedDesiredIp = [Regex]::Escape($desiredIp)
$escapedCertHost = [Regex]::Escape($certHost)

$patternToMatch = ".*$escapedDesiredIp\s+$escapedCertHost.*"

If($hostsFileContent -match $patternToMatch)
{
	write-host 'Entry already set!'
}
Else
{
	write-host $desiredIp.PadRight(20," ") "$certHost - adding to hosts file ..."
    Add-Content -Encoding UTF8  $hostsFilePath ("`n$desiredIp $certHost")
}

$selfSignedCerts = Get-ChildItem -Path cert:\localMachine\my\ | Where-Object { $_.subject -eq "CN=$certHost"}

# remove existing certificates
If($selfSignedCerts) {
	Write-Host 'Removing old certificates...'
	$selfSignedCerts | Remove-Item
} 

# generate self signed certificate
New-SelfSignedCertificate -DnsName $certHost -CertStoreLocation "cert:\LocalMachine\My"

# export to pfx
$pwd = ConvertTo-SecureString -String $certPassword -Force -AsPlainText
Get-ChildItem -Path cert:\localMachine\my\ | Where-Object { $_.subject -eq "CN=$certHost"} | Export-PfxCertificate -FilePath "$solutionPath/$certName" -Password $pwd



# import certificate
# $pwd = ConvertTo-SecureString -String $certPassword -Force -AsPlainText
Import-PfxCertificate -FilePath "$solutionPath/$certName" -CertStoreLocation cert:\LocalMachine\Root -Password $pwd

# install service
If(Get-Service -Name $serviceName -ErrorAction SilentlyContinue)
{
	write-host 'Service '$serviceName' is already installed!'
}
Else
{
	write-host 'Creating service '$serviceName' ...'
	New-Service -Name $serviceName -BinaryPathName "$solutionPath\$projectName.exe --cert-host $certHost --cert-path $solutionPath/$certName --cert-password $certPassword $portNumber $portNumberSsl"
}

# run service
write-host 'Running service '$serviceName' ...'
Set-Service -Name $serviceName -Status Running

# restore directory
cd $currentDirectory

write-host 'Service '$serviceName' has started!'