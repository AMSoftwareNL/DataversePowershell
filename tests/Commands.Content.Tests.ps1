BeforeAll {
	$TestDirectory = Split-Path $PSCommandPath -Parent
	$RootDirectory = Split-Path $TestDirectory -Parent

	$ModuleDirectory = Resolve-Path -Path (Join-Path -Path $RootDirectory -ChildPath '.\dist\AMSoftware.Dataverse.PowerShell\')
	$ModulePath = Join-Path -Path $ModuleDirectory -ChildPath 'AMSoftware.Dataverse.PowerShell.psd1'

	Import-Module -Name $ModulePath -Scope Local -Force

	Connect-DataverseEnvironment -EnvironmentUrl $env:DATAVERSEURL_TEST -ClientId $env:DATAVERSECLIENTID_TEST -ClientSecret (ConvertTo-SecureString -String $env:DATAVERSECLIENTSECRET_TEST -AsPlainText -Force)
}

AfterAll {
	Remove-Module 'AMSoftware.Dataverse.PowerShell'
}

Describe 'Commands.Content' -Tag 'Integration' {
	Context 'Add-DataverseRow' {
		It 'Adds row with values' {
			$values = @{
				'name' = 'Test Account'
			}

			$result = Add-DataverseRow -Table 'account' -Values $values
			$result | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'

			$row = $result | Get-DataverseRow
			$row.Name | Should -Be 'Test Account'
		}

		It 'Adds row with inputobject' {
			$inputobject = [dvrow]::new('account')
			$inputobject.name = 'Test Account'

			$result = $inputobject | Add-DataverseRow
			$result | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'

			$row = $result | Get-DataverseRow
			$row.Name | Should -Be 'Test Account'
		}

		It 'Upserts row with values' {
			$rowid = [guid]::NewGuid()

			$created = Add-DataverseRow -Table 'account' -Id $rowid -Values @{ name = 'Test Account (Created)' }
			$created | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
			$created.Id | Should -Be $rowid

			$row = Get-DataverseRow -Table 'account' -Id $rowid
			$row.Name | Should -Be 'Test Account (Created)'

			$updated = Add-DataverseRow -Table 'account' -Id $rowid -Values @{ name = 'Test Account (Updated)' }
			$updated | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
			$updated.Id | Should -Be $rowid

			$row = Get-DataverseRow -Table 'account' -Id $rowid
			$row.Name | Should -Be 'Test Account (Updated)'
		}

		It 'Adds rows with Batch' {
			$batchid = Request-DataverseBatch -Name 'Batch for Pester' -ReturnResults
			$batchid | Should -BeOfType 'System.Guid'
			$batchid | Should -Not -BeNull
			$batchid | Should -Not -Be [guid]::Empty
			
			$output = Add-DataverseRow -Table 'account' -Values @{ name = 'Test Account (Batch)' } -BatchId $batchid
			$output | Should -BeNull

			$batchResult = Submit-DataverseBatch -BatchId $batchid

			$batchResult | Should -HaveCount 1
			$batchResult.Response | Should -BeOfType 'Microsoft.Xrm.Sdk.Messages.CreateResponse'
		}

		It 'Adds values through pipeline' {
			$output = 1..10 | ForEach-Object { $a = @{ name = "Account $_" }; $a } | Add-DataverseRow -Table 'account'

			$output | Should -HaveCount 10
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
		}
	}
	
	Context 'Add-DataverseRows' {
		It 'Adds 500 rows from Entity' {
			$output = 1..500 | ForEach-Object { $row = [dvrow]::new('account'); $row.name = "Multiple Account $_"; $row } | Add-DataverseRows

			$output | Should -HaveCount 500
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
		}

		It 'Adds 500 rows from Values' {
			$output = 1..500 | ForEach-Object { $row = @{ name = "Multiple Account $_" }; $row } | Add-DataverseRows -Table 'account'

			$output | Should -HaveCount 500
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
		}

		It 'Upserts 500 rows from Entity' {
			$output = 1..500 | ForEach-Object { $row = [dvrow]::new('account'); $row.name = "Multiple Account $_"; $row } | Add-DataverseRows -Upsert

			$output | Should -HaveCount 500
			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.EntityReference'
		}
	}
	
	Context 'Add-DataverseRelatedRows' {
		
	}

	Context 'Get-DataverseRows' {

		It "Retrieve With FetchXml" {
			[xml]$fetchxml = 
@"
<fetch>
	<entity name='account'>
		<attribute name='name' />
	</entity>
</fetch>
"@
			$output = Get-DataverseRows -FetchXML $fetchxml

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -Not -HaveCount 0
		}

		It "Retrieve With FetchXml (Top 50)" {
			[xml]$fetchxml = 
@"
<fetch>
	<entity name='account'>
		<attribute name='name' />
	</entity>
</fetch>
"@
			$output = Get-DataverseRows -FetchXML $fetchxml -Top 50

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -HaveCount 50
		}

        It "Retrieve With AttributeQuery" {
			$output = Get-DataverseRows -Table 'account' -Columns 'name' -Sort @{name=[Microsoft.Xrm.Sdk.Query.OrderType]::Ascending} -Query @{name='Account 1'}

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -Not -HaveCount 0

			$output.Name | ForEach-Object { Should -ActualValue $_ -Be 'Account 1' }
		}

        It "Retrieve With Query" {
			$output = Get-DataverseRows -Table 'account' -Columns 'name' -Sort @{name=[Microsoft.Xrm.Sdk.Query.OrderType]::Ascending}

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -Not -HaveCount 0
		}

		It "Retrieve With Query (top 50)" {
			$output = Get-DataverseRows -Table 'account' -Columns 'name' -Sort @{name=[Microsoft.Xrm.Sdk.Query.OrderType]::Ascending} -Top 50

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -HaveCount 50
		}

        It "Retrieve With Batch" {
			$inputs = Get-DataverseRows -Table 'account' -Columns 'accountid' -Top 50

			$output = $inputs | Get-DataverseRows -Table 'account'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -HaveCount 50
		}
	}

	Context 'Get-DataverseRow' {
		It "Retrieve With Id" {
			$inputs = Get-DataverseRows -Table 'account' -Columns 'accountid' -Top 50

			$output = $inputs | Get-DataverseRow -Table 'account' -Columns 'name'

			$output | Should -BeOfType 'Microsoft.Xrm.Sdk.Entity'
			$output | Should -HaveCount 50
		}
	}

	Context 'Remove-DataverseRelatedRow' {
	}

	Context 'Remove-DataverseRow' {
		It "Removes rows" {
			$inputs = Get-DataverseRows -Table 'account' -Columns 'accountid' -Top 50

			$inputs | Remove-DataverseRow -Table 'account' -Force

			{ $inputs | Get-DataverseRows -Table 'account' -ErrorAction Stop } | Should -Throw '*Does not Exist*'
		}
	}

	Context 'Remove-DataverseRows' {
		It "Removes rows on standard table" {
			$inputs = Get-DataverseRows -Table 'account' -Columns 'accountid' -Top 50

			{ $inputs | Remove-DataverseRows -Table 'account' -Force -ErrorAction Stop } | Should -Throw "The 'DeleteMultiple' method does not support entities of type 'account'*"
		}
	}

	Context 'Send-DataverseRequest' {
		It "Send Unbound" {
			$output = Send-DataverseRequest -Name 'WhoAmI'

			$output.UserId | Should -Not -BeNullOrEmpty
			$output.BusinessUnitId | Should -Not -BeNullOrEmpty
			$output.OrganizationId | Should -Not -BeNullOrEmpty
		} 

		It "Send Unbound with parameters" {
			$randomform = Get-DataverseRows -Table 'systemform' -Query @{objecttypecode='account';type=2} -Top 1

			$output = Send-DataverseRequest -Name 'CopySystemForm' -Parameters @{SourceId=$randomform.Id}
			$output.Id | Should -Not -BeNullOrEmpty
		}

		It "Send Bound" {
			$row = Get-DataverseRows -Table 'account' -Top 1

			$output = $row | Send-DataverseRequest -Name 'GenerateSharedLink' -Parameters @{SharedRights=[Microsoft.Crm.Sdk.Messages.AccessRights]::ReadAccess}
			$output | Should -Not -Be $null
		}
	}

	Context 'Set-DataverseRow' {
		It "Set from InputObject" {
			$row = Get-DataverseRows -Table 'account' -Top 1
			$row.name='Account Updated'

			$output = $row | Set-DataverseRow

			$output.Id | Should -Be $row.Id
		}

		It "Set from Values" {
			$row = Get-DataverseRows -Table 'account' -Top 1

			$output = Set-DataverseRow -Table $row.LogicalName -Id $row.Id -Values @{name='Account Updated'}

			$output.Id | Should -Be $row.Id
		}
	}

	Context 'Set-DataverseRows' {
		It "Set from InputObject" {
			$row = Get-DataverseRows -Table 'account' -Top 1
			$row.name='Account Updated'

			$output = $row | Set-DataverseRows

			$output.Id | Should -Be $row.Id
		}

		It "Set from Values" {
			$row = Get-DataverseRows -Table 'account' -Top 1

			$output = $row | Set-DataverseRow -Values @{name='Account Updated'}

			$output.Id | Should -Be $row.Id
		}
	}
}

