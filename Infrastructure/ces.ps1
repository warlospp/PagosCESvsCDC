function Create-SasToken {
    param (
        [string]$resourceUri,
        [string]$keyName,
        [string]$key
    )
    $sinceEpoch = [datetime]::UtcNow - [datetime]"1970-01-01"
    $expiry = [int]$sinceEpoch.TotalSeconds + 3600*24*180
    $stringToSign = [System.Web.HttpUtility]::UrlEncode($resourceUri) + "`n" + $expiry
    $hmac = New-Object System.Security.Cryptography.HMACSHA256
    $hmac.Key = [Text.Encoding]::UTF8.GetBytes($key)
    $signature = [Convert]::ToBase64String($hmac.ComputeHash([Text.Encoding]::UTF8.GetBytes($stringToSign)))
    $sasToken = "SharedAccessSignature sr=$([System.Web.HttpUtility]::UrlEncode($resourceUri))&sig=$([System.Web.HttpUtility]::UrlEncode($signature))&se=$expiry&skn=$keyName"
    return $sasToken
}

$resourceUri = "https://arquitecturadatosdemoces.servicebus.windows.net/pagos_ces"  
$keyName = "xxx"
$key = "xxx"
$sasToken = Create-SasToken -resourceUri $resourceUri -keyName $keyName -key $key
Write-Output $sasToken