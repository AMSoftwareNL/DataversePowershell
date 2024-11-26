BeforeAll {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath '.\dist\AMSoftware.Dataverse.PowerShell\')
	$ModulePath   = Join-Path -Path $ModuleDirectory -ChildPath 'AMSoftware.Dataverse.PowerShell.psd1'

	Import-Module -Name $ModulePath -Scope Local -Force

	Connect-DataverseEnvironment -EnvironmentUrl $env:DATAVERSEURL_TEST -ClientId $env:DATAVERSECLIENTID_TEST -ClientSecret (ConvertTo-SecureString -String $env:DATAVERSECLIENTSECRET_TEST -AsPlainText -Force)
}

AfterAll {
	Remove-Module 'AMSoftware.Dataverse.PowerShell'
}

Describe "Commands.Content" {
	Context "Add-DataverseRow" {
		It "Adds row with values" {
			$values = @{
				'name'='Test Account'
			}

			$result = Add-DataverseRow -Table 'account' -Values $values
			$result | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'

			$row = $result | Get-DataverseRow
			$row.Name | Should -Be 'Test Account'
		}

		It "Adds row with inputobject" {
			$input = [dvrow]::new('account')
			$input.name = 'Test Account'

			$result = $input | Add-DataverseRow
			$result | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'

			$row = $result | Get-DataverseRow
			$row.Name | Should -Be 'Test Account'
		}

		It "Upserts row with values" {
			$rowid = [guid]::NewGuid()


			$created = Add-DataverseRow -Table 'account' -Id $rowid -Values @{ name='Test Account (Created)' }
			$created | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
			$created.Id | Should -Be $rowid

			$row = Get-DataverseRow -Table 'account' -Id $rowid
			$row.Name | Should -Be 'Test Account (Created)'

			$updated = Add-DataverseRow -Table 'account' -Id $rowid -Values @{ name='Test Account (Updated)' }
			$updated | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
			$updated.Id | Should -Be $rowid

			$row = Get-DataverseRow -Table 'account' -Id $rowid
			$row.Name | Should -Be 'Test Account (Updated)'
		}

		It "Adds rows with Batch" {
			$batchid = Request-DataverseBatch -Name 'Batch for Pester' -ReturnResults
			$batchid | Should -BeOfType 'System.Guid'
			$batchid | Should -Not -BeNull
			$batchid | Should -Not -Be [guid]::Empty
			
			$output = Add-DataverseRow -Table 'account' -Values @{ name='Test Account (Batch)' } -BatchId $batchid
			$output | Should -BeNull

			$batchResult = Submit-DataverseBatch -BatchId $batchid

			,$batchResult.Count | Should -Be 1
			$batchResult.Response | Should -BeOfType 'Microsoft.Xrm.Sdk.Messages.CreateResponse'
		}
	}
	
	Context "Add-DataverseRows" {
		It "Adds 500 rows as Multiple" {
			$inputs =  1..500 | Foreach-Object { $row = [dvrow]::new('account'); $row.name = "Multiple Account $_"; $row }

			$output = $inputs | Add-DataverseRows 

			,$output.Count | Should -Be 500
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
		}
	}
	
	Context "Add-DataverseRelatedRows" {

	}

	Context "Get-DataverseRows" {

	}

	Context "Get-DataverseRow" {
	}

	Context "Remove-DataverseRrelatedRow" {
	}

	Context "Remove-DataverseRow" {
	}

	Context "Send-DataverseRequest" {
	}

	Context "Set-DataverseRow" {
	}

	Context "Set-DataverseRows" {
	}
}

