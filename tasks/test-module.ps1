$testConfig = New-PesterConfiguration
$testConfig.Run.Path = './tests/'
$testConfig.Filter.ExcludeTag = 'Integration'
$testConfig.CodeCoverage.Enabled = $true
$testConfig.CodeCoverage.OutputPath = './dist/codecoverage.xml'
$testConfig.CodeCoverage.Path = './dist/AMSoftware.Dataverse.PowerShell'
$testConfig.TestResult.Enabled = $true
$testConfig.TestResult.OutputPath = './dist/testresults.xml'

Invoke-Pester -Configuration $testConfig