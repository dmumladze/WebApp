$block = {
    for ($i = 0; $i -lt 100; $i++) {
        Invoke-RestMethod -Uri https://localhost:44330/jobs
        #Start-Sleep -Milliseconds 25
    }
}
  
$job1 = Start-ThreadJob -ScriptBlock $block
$job2 = Start-ThreadJob -ScriptBlock $block
$job3 = Start-ThreadJob -ScriptBlock $block

Wait-Job -Job $job1 | Out-Null
Wait-Job -Job $job2 | Out-Null
Wait-Job -Job $job3 | Out-Null

$results = @(
    Receive-Job -Job $job1
    Receive-Job -Job $job2
    Receive-Job -Job $job3
)

$dict = @{};

foreach ($result in $results | Sort-Object -Property start) {
    $dict.add($result.jobId, $result.threadId) #throws exception if dup key added
    Write-Output "$($result.jobId)-::-$($result.threadId)-::-$(Get-Date $result.start -Format "yyyy:MM:ddTHH:mm:ss.fffffZ")-::-$(Get-Date $result.end -Format "yyyy:MM:ddTHH:mm:ss.fffffZ")"
}


