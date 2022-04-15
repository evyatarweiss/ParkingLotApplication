


$LoginExplenation = "In order to deploy the ARM template to azure you need to Login to your azure account \n (Please make sure that you have an account with the free trail))"
Write-Output $LoginExplenation
az login --use-device-code

Write-Output "Looking for available location to create the Resource Group"
$locations = az account list-locations | ConvertFrom-Json
$choosenLocation =  $locations[0].name
Write-Output "This is the choosen location : "$choosenLocation

$rgName = "EvyatarAlonTamirResourceGroup"
Write-Output "The resource group name is : "$rgName

az group create --name $rgName --location $choosenLocation `

az deployment group create --resource-group $rgName `
--parameters language=".net" ParkingLotApplication="true" webAppName="joysworld" `
--name HW1CloudComputingAssignment `
--template-uri "https://github.com/evyatarweiss/ParkingLotApplication/blob/main/HW1ArmTemplate.json"