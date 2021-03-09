$block = {
    for ($i = 0; $i -lt 4; $i++) {
        Invoke-RestMethod -Uri https://localhost:44330/jobs | Format-Table -Property jobId, threadId, timestamp
    }
}
  
$job1 = Start-Job -Name "J1" -ScriptBlock $block
$job2 = Start-Job -Name "J2" -ScriptBlock $block
$job3 = Start-Job -Name "J3" -ScriptBlock $block

Wait-Job -Job $job1 | Out-Null
Wait-Job -Job $job2 | Out-Null
Wait-Job -Job $job3 | Out-Null

Receive-Job -Job $job1
Receive-Job -Job $job2
Receive-Job -Job $job3